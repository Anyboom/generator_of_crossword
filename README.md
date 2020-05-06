# generator_of_crossword

## Description

This framework generate classic crossword

## Example

```csharp

GeneratorCrossword crossWord = new GeneratorCrossword(words, sizeX, sizeY);

crossWord.Generate();

char?[,] Field = crossWord.Field;

if ( Directory.Exists("Result") == false)
{
    Directory.CreateDirectory("Result");
}

using ( StreamWriter streamOpen = new StreamWriter("Result/Used_words.txt") )
{
    foreach ( string item in crossWord.ListOfUseWords )
    {
        await streamOpen.WriteLineAsync(item);
    }
}

using ( StreamWriter streamOpen = new StreamWriter("Result/Not_used_words.txt") )
{
    foreach ( string item in crossWord.ListOfExcessWords )
    {
        await streamOpen.WriteLineAsync(item);
    }
}

using ( StreamWriter streamOpen = new StreamWriter(new FileStream("Result/Crossword.csv", FileMode.Create), Encoding.Default) )
{
    for ( int x = 0; x < Field.GetLength(0); x++ )
    {
        for ( int y = 0; y < Field.GetLength(1); y++ )
        {
            await streamOpen.WriteAsync(Field[x, y].ToString() + ";");
        }
        await streamOpen.WriteLineAsync();
    }
}
```
