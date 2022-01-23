public class Attributes
{
    public string AttributeName;
    public int    FOR;
    public int    INT;
    public int    VIT;
    public int    AGI;

    public Attributes(string strAttributeName, int FOR, int INT, int VIT, int AGI){
        AttributeName = strAttributeName;
        this.FOR      = FOR;
        this.INT      = INT;
        this.VIT      = VIT;
        this.AGI      = AGI;
    }

    public override string ToString()
    {
        return AttributeName + "; Força=" + FOR.ToString() 
                             + "; Inteligência=" + INT.ToString() 
                             + "; Vitalidade=" + VIT.ToString() 
                             + "; Agilidade=" + AGI.ToString();
    }
}
