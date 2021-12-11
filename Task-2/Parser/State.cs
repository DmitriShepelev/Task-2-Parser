namespace Task_2.Parser
{
    enum State
    {
        Neutral,
        Word,        
        WhiteSpace,
        InternalPunctuation,
        TrailingPunctuation,
        ControlSymbol
    }
}
