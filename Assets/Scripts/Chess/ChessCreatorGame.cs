using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Prototype d'echecs personnalisables. Place ce script sur un GameObject vide.
/// L'interface est dessinee avec OnGUI : aucun Canvas n'est necessaire.
/// </summary>
public class ChessCreatorGame : MonoBehaviour
{
    public enum MovementType { Pawn, Rook, Bishop, Knight, Queen, King, Custom }
    public enum PlayEffect { None, DamageAdjacentEnemies, HealSelf, ShieldAdjacentAllies, PoisonAdjacentEnemies, HealAdjacentAllies, GainAttack }
    public enum SquareEffect { None, Fire, Poison, Sanctuary }

    [Serializable]
    public class PieceDefinition
    {
        public string pieceName = "Nouveau pion";
        public int attack = 2;
        public int defense = 1;
        public int maxHP = 5;
        public MovementType movement = MovementType.Pawn;
        public int customRange = 1;
        public bool customStraight = true;
        public bool customDiagonal = false;
        public bool customCanJump = false;
        public PlayEffect onPlay = PlayEffect.None;
        public SquareEffect squareOnMove = SquareEffect.None;
        public PlayEffect onTurnEnd = PlayEffect.None;
    }

    [Serializable]
    public class PieceData
    {
        public int definitionIndex;
        public bool white;
        public int x, y, hp, attackBonus, shield;
        public bool poisoned;
        public int poisonTurns;
        public bool alive = true;
    }

    [Serializable] public class DefinitionList { public List<PieceDefinition> items = new List<PieceDefinition>(); }

    public List<PieceDefinition> definitions = new List<PieceDefinition>();
    private PieceData[,] board = new PieceData[8, 8];
    private int selectedX = -1, selectedY = -1;
    private bool whiteTurn = true;
    private bool editorOpen;
    private int editingIndex;
    private string status = "Blancs : selectionne un pion puis une case.";
    private Vector2 editorScroll;
    private string saveFile { get { return Path.Combine(Application.persistentDataPath, "chess_creator_pieces.json"); } }

    void Start()
    {
        if (definitions.Count == 0) CreateDefaultDefinitions();
        ResetBoard();
    }

    void CreateDefaultDefinitions()
    {
        definitions.Add(new PieceDefinition { pieceName="Pion", attack=1, defense=0, maxHP=3, movement=MovementType.Pawn });
        definitions.Add(new PieceDefinition { pieceName="Tour", attack=3, defense=2, maxHP=7, movement=MovementType.Rook });
        definitions.Add(new PieceDefinition { pieceName="Fou", attack=3, defense=1, maxHP=5, movement=MovementType.Bishop });
        definitions.Add(new PieceDefinition { pieceName="Cavalier", attack=4, defense=1, maxHP=5, movement=MovementType.Knight });
        definitions.Add(new PieceDefinition { pieceName="Reine", attack=4, defense=2, maxHP=8, movement=MovementType.Queen });
        definitions.Add(new PieceDefinition { pieceName="Roi", attack=2, defense=3, maxHP=10, movement=MovementType.King });
    }

    void ResetBoard()
    {
        board = new PieceData[8, 8];
        for (int x = 0; x < 8; x++)
        {
            Place(x, 1, 0, true); Place(x, 6, 0, false);
        }
        int[] back = { 1, 3, 2, 4, 5, 2, 3, 1 };
        for (int x = 0; x < 8; x++) { Place(x, 0, back[x], true); Place(x, 7, back[x], false); }
        whiteTurn = true; selectedX = selectedY = -1;
        status = "Nouvelle partie. Blancs commencent.";
    }

    void Place(int x, int y, int def, bool white)
    {
        if (def < 0 || def >= definitions.Count) return;
        PieceDefinition d = definitions[def];
        board[x, y] = new PieceData { x=x, y=y, definitionIndex=def, white=white, hp=d.maxHP, alive=true };
    }

    void OnGUI()
    {
        GUI.skin.label.fontSize = 14;
        GUI.skin.button.fontSize = 13;
        GUILayout.BeginHorizontal();
        DrawBoard();
        GUILayout.BeginVertical(GUILayout.Width(310));
        GUILayout.Label("CHESS CREATOR", BigLabel());
        GUILayout.Label("Tour : " + (whiteTurn ? "Blancs" : "Noirs"));
        GUILayout.Label(status, GUILayout.MinHeight(44));
        if (GUILayout.Button("Nouvelle partie", GUILayout.Height(30))) ResetBoard();
        if (GUILayout.Button(editorOpen ? "Fermer l'editeur" : "Editeur de pions", GUILayout.Height(30))) editorOpen = !editorOpen;
        if (GUILayout.Button("Sauvegarder les definitions", GUILayout.Height(28))) SaveDefinitions();
        if (GUILayout.Button("Charger les definitions", GUILayout.Height(28))) LoadDefinitions();
        if (editorOpen) DrawEditor();
        else DrawSelectedInfo();
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }

    GUIStyle BigLabel()
    {
        GUIStyle s = new GUIStyle(GUI.skin.label); s.fontSize = 22; s.fontStyle = FontStyle.Bold; return s;
    }

    void DrawBoard()
    {
        GUILayout.BeginVertical(GUILayout.Width(520));
        for (int y = 7; y >= 0; y--)
        {
            GUILayout.BeginHorizontal();
            for (int x = 0; x < 8; x++)
            {
                Color old = GUI.backgroundColor;
                GUI.backgroundColor = ((x + y) % 2 == 0) ? new Color(.82f,.78f,.68f) : new Color(.35f,.42f,.35f);
                if (x == selectedX && y == selectedY) GUI.backgroundColor = new Color(.95f,.75f,.2f);
                string label = " ";
                PieceData p = board[x,y];
                if (p != null && p.alive)
                {
                    string[] symbols = { "P", "R", "B", "N", "Q", "K", "C" };
                    string sym = p.definitionIndex < symbols.Length ? symbols[p.definitionIndex] : "?";
                    label = (p.white ? "W" : "B") + sym + "\n" + p.hp + " PV";
                    if (p.shield > 0) label += "\nBouclier " + p.shield;
                    if (p.poisoned) label += "\nPoison";
                }
                if (GUILayout.Button(label, GUILayout.Width(62), GUILayout.Height(58))) HandleSquareClick(x,y);
                GUI.backgroundColor = old;
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
    }

    void DrawSelectedInfo()
    {
        if (selectedX < 0) { GUILayout.Label("Selectionne un pion pour voir ses stats."); return; }
        PieceData p = board[selectedX,selectedY];
        if (p == null) return;
        PieceDefinition d = definitions[p.definitionIndex];
        GUILayout.Space(12); GUILayout.Label("Pion selectionne : " + d.pieceName);
        GUILayout.Label("Attaque : " + (d.attack + p.attackBonus) + " | Defense : " + d.defense);
        GUILayout.Label("PV : " + p.hp + "/" + d.maxHP + " | Bouclier : " + p.shield);
        GUILayout.Label("Deplacement : " + d.movement);
        GUILayout.Label("Effet activation : " + d.onPlay);
        GUILayout.Label("Effet de case : " + d.squareOnMove);
        GUILayout.Label("Effet fin de tour : " + d.onTurnEnd);
    }

    void DrawEditor()
    {
        GUILayout.Space(8); GUILayout.Label("EDITEUR DE PIONS", BigLabel());
        if (definitions.Count == 0) { GUILayout.Label("Aucune definition."); return; }
        editorScroll = GUILayout.BeginScrollView(editorScroll, GUILayout.Height(430));
        editingIndex = Mathf.Clamp(editingIndex, 0, definitions.Count - 1);
        string[] names = new string[definitions.Count];
        for (int i=0;i<definitions.Count;i++) names[i] = i + ". " + definitions[i].pieceName;
        GUILayout.Label("Definition a modifier");
        if (GUILayout.Button("Definition : " + names[editingIndex] + " (cliquer pour suivante)")) editingIndex = (editingIndex + 1) % definitions.Count;
        PieceDefinition d = definitions[editingIndex];
        d.pieceName = TextField("Nom", d.pieceName);
        d.attack = IntField("Attaque", d.attack);
        d.defense = IntField("Defense", d.defense);
        d.maxHP = Mathf.Max(1, IntField("Points de vie max", d.maxHP));
        d.movement = EnumField("Deplacement", d.movement);
        if (d.movement == MovementType.Custom)
        {
            d.customRange = Mathf.Clamp(IntField("Portee max", d.customRange), 1, 7);
            d.customStraight = GUILayout.Toggle(d.customStraight, "Deplacement en ligne");
            d.customDiagonal = GUILayout.Toggle(d.customDiagonal, "Deplacement diagonal");
            d.customCanJump = GUILayout.Toggle(d.customCanJump, "Peut sauter par-dessus les pieces");
        }
        d.onPlay = EnumField("Effet a l'activation", d.onPlay);
        d.squareOnMove = EnumField("Effet de la case", d.squareOnMove);
        d.onTurnEnd = EnumField("Effet de fin de tour", d.onTurnEnd);
        if (GUILayout.Button("Ajouter une definition")) { definitions.Add(new PieceDefinition()); editingIndex = definitions.Count - 1; }
        if (GUILayout.Button("Supprimer cette definition") && definitions.Count > 1)
        {
            definitions.RemoveAt(editingIndex); editingIndex = Mathf.Clamp(editingIndex,0,definitions.Count-1);
        }
        GUILayout.Label("Les pieces deja sur le plateau gardent leur index de definition. Evite de supprimer des definitions pendant une partie.");
        GUILayout.EndScrollView();
    }

    string TextField(string label, string value)
    {
        GUILayout.BeginHorizontal(); GUILayout.Label(label, GUILayout.Width(145)); value = GUILayout.TextField(value); GUILayout.EndHorizontal(); return value;
    }
    int IntField(string label, int value)
    {
        GUILayout.BeginHorizontal(); GUILayout.Label(label, GUILayout.Width(145)); string s = GUILayout.TextField(value.ToString()); int n; if (int.TryParse(s,out n)) value=n; GUILayout.EndHorizontal(); return value;
    }
    T EnumField<T>(string label, T value) where T : struct
    {
        GUILayout.BeginHorizontal(); GUILayout.Label(label, GUILayout.Width(145));
        string[] opts = Enum.GetNames(typeof(T)); int current = Array.IndexOf(opts, value.ToString());
        int next = Mathf.Max(0, current);
        if (opts.Length > 0) next = GUILayout.SelectionGrid(next, opts, 1);
        if (next >= 0 && next < opts.Length) value = (T)Enum.Parse(typeof(T), opts[next]);
        GUILayout.EndHorizontal(); return value;
    }

    void HandleSquareClick(int x, int y)
    {
        PieceData clicked = board[x,y];
        if (selectedX < 0)
        {
            if (clicked != null && clicked.white == whiteTurn) { selectedX=x; selectedY=y; status="Pion selectionne. Clique sur une destination."; }
            else status="Selectionne un de tes pions.";
            return;
        }
        PieceData source = board[selectedX,selectedY];
        if (source == null) { selectedX=selectedY=-1; return; }
        if (clicked != null && clicked.white == whiteTurn)
        {
            selectedX=x; selectedY=y; status="Selection modifiee."; return;
        }
        if (!CanMove(source, x, y)) { status="Deplacement impossible pour ce pion."; return; }
        MovePiece(source,x,y);
    }

    bool CanMove(PieceData p, int tx, int ty)
    {
        int dx=tx-p.x, dy=ty-p.y, ax=Mathf.Abs(dx), ay=Mathf.Abs(dy);
        if (dx==0 && dy==0) return false;
        PieceData target=board[tx,ty]; if (target!=null && target.white==p.white) return false;
        PieceDefinition d=definitions[p.definitionIndex];
        switch(d.movement)
        {
            case MovementType.Knight: return (ax==1 && ay==2)||(ax==2 && ay==1);
            case MovementType.King: return ax<=1 && ay<=1;
            case MovementType.Rook: if(dx!=0 && dy!=0)return false; break;
            case MovementType.Bishop: if(ax!=ay)return false; break;
            case MovementType.Queen: if(dx!=0 && dy!=0 && ax!=ay)return false; break;
            case MovementType.Pawn:
                int dir=p.white?1:-1;
                if(target!=null) return ax==1 && dy==dir;
                if(dx!=0)return false;
                if(dy==dir)return true;
                if(dy==2*dir && ((p.white&&p.y==1)||(!p.white&&p.y==6)) && board[p.x,p.y+dir]==null)return true;
                return false;
            case MovementType.Custom:
                if(Mathf.Max(ax,ay)>d.customRange)return false;
                bool straight=(dx==0||dy==0)&&d.customStraight;
                bool diagonal=(ax==ay)&&d.customDiagonal;
                if(!straight&&!diagonal)return false;
                break;
        }
        if(d.movement!=MovementType.Knight && !(d.movement==MovementType.Custom && d.customCanJump) && !ClearPath(p.x,p.y,tx,ty)) return false;
        return true;
    }

    bool ClearPath(int x0,int y0,int x1,int y1)
    {
        int sx=Math.Sign(x1-x0), sy=Math.Sign(y1-y0); int x=x0+sx,y=y0+sy;
        while(x!=x1 || y!=y1) { if(board[x,y]!=null)return false; x+=sx; y+=sy; }
        return true;
    }

    void MovePiece(PieceData p,int x,int y)
    {
        bool movingSide = whiteTurn;
        PieceData target=board[x,y];
        if(target!=null)
        {
            Attack(p,target);
            // Si la cible survit au combat, l'attaquant reste sur sa case.
            if(target.alive)
            {
                selectedX=selectedY=-1;
                whiteTurn=!whiteTurn;
                ApplyEndTurnEffects(movingSide);
                status="La cible survit au combat et bloque la case. " + (whiteTurn?"Blancs":"Noirs") + " jouent.";
                return;
            }
        }
        board[p.x,p.y]=null; p.x=x;p.y=y;board[x,y]=p;
        ApplySquareEffect(p, definitions[p.definitionIndex].squareOnMove);
        if(p.alive) ApplyEffect(p, definitions[p.definitionIndex].onPlay);
        selectedX=selectedY=-1;
        whiteTurn=!whiteTurn;
        ApplyEndTurnEffects(movingSide);
        status=(whiteTurn?"Blancs":"Noirs")+" jouent. Dernier coup : "+definitions[p.definitionIndex].pieceName+".";
    }

    void Attack(PieceData attacker, PieceData target)
    {
        PieceDefinition ad=definitions[attacker.definitionIndex], td=definitions[target.definitionIndex];
        int damage=Mathf.Max(1, ad.attack+attacker.attackBonus-td.defense-target.shield);
        target.hp-=damage; target.shield=0;
        if(target.hp<=0)target.alive=false;
        status=ad.pieceName+" inflige "+damage+" degats a "+td.pieceName+".";
    }

    void ApplyEffect(PieceData p, PlayEffect effect)
    {
        switch(effect)
        {
            case PlayEffect.DamageAdjacentEnemies: AffectNeighbours(p, delegate(PieceData n){if(n.white!=p.white){n.hp-=Mathf.Max(1,definitions[p.definitionIndex].attack);if(n.hp<=0)n.alive=false;}}); break;
            case PlayEffect.HealSelf: p.hp=Mathf.Min(definitions[p.definitionIndex].maxHP,p.hp+2); break;
            case PlayEffect.ShieldAdjacentAllies: AffectNeighbours(p,delegate(PieceData n){if(n.white==p.white)n.shield+=2;}); break;
            case PlayEffect.PoisonAdjacentEnemies: AffectNeighbours(p,delegate(PieceData n){if(n.white!=p.white){n.poisoned=true;n.poisonTurns=2;}}); break;
            case PlayEffect.HealAdjacentAllies: AffectNeighbours(p,delegate(PieceData n){if(n.white==p.white)n.hp=Mathf.Min(definitions[n.definitionIndex].maxHP,n.hp+2);}); break;
            case PlayEffect.GainAttack: p.attackBonus++; break;
        }
    }

    void ApplySquareEffect(PieceData p, SquareEffect effect)
    {
        if(effect==SquareEffect.Fire || effect==SquareEffect.Poison)
        {
            // Les cases dangereuses s'appliquent immediatement au pion qui vient d'y arriver.
            if(effect==SquareEffect.Fire){p.hp-=2;if(p.hp<=0)p.alive=false;}
            else {p.poisoned=true;p.poisonTurns=Math.Max(p.poisonTurns,2);}
        }
        else if(effect==SquareEffect.Sanctuary) p.hp=Mathf.Min(definitions[p.definitionIndex].maxHP,p.hp+2);
    }

    void AffectNeighbours(PieceData center, Action<PieceData> action)
    {
        for(int x=Mathf.Max(0,center.x-1);x<=Mathf.Min(7,center.x+1);x++)
            for(int y=Mathf.Max(0,center.y-1);y<=Mathf.Min(7,center.y+1);y++)
                if(!(x==center.x&&y==center.y) && board[x,y]!=null && board[x,y].alive) action(board[x,y]);
    }

    void ApplyEndTurnEffects(bool sideWhoseTurnStarts)
    {
        for(int x=0;x<8;x++)for(int y=0;y<8;y++)
        {
            PieceData p=board[x,y]; if(p==null||!p.alive)continue;
            if(p.poisoned && p.poisonTurns>0)
            {
                p.hp--; p.poisonTurns--; if(p.poisonTurns<=0)p.poisoned=false; if(p.hp<=0){p.alive=false;board[x,y]=null;continue;}
            }
            if(p.white==sideWhoseTurnStarts) ApplyEffect(p,definitions[p.definitionIndex].onTurnEnd);
        }
        // Nettoyage des pieces mortes.
        for(int x=0;x<8;x++)for(int y=0;y<8;y++)if(board[x,y]!=null&&!board[x,y].alive)board[x,y]=null;
    }

    void SaveDefinitions()
    {
        try { DefinitionList wrapper=new DefinitionList(); wrapper.items=definitions; File.WriteAllText(saveFile,JsonUtility.ToJson(wrapper,true)); status="Definitions sauvegardees : "+saveFile; }
        catch(Exception e){status="Erreur sauvegarde : "+e.Message;}
    }
    void LoadDefinitions()
    {
        try
        {
            if(!File.Exists(saveFile)){status="Aucun fichier de sauvegarde trouve.";return;}
            DefinitionList wrapper=JsonUtility.FromJson<DefinitionList>(File.ReadAllText(saveFile));
            if(wrapper!=null&&wrapper.items!=null&&wrapper.items.Count>0){definitions=wrapper.items; ResetBoard();status="Definitions chargees.";}
        }
        catch(Exception e){status="Erreur chargement : "+e.Message;}
    }
}
