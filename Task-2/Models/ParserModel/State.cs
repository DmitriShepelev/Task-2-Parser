namespace Task_2.ParserModel
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
