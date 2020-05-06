# generator_of_crossword

## Описание

This framework generate classic crossword

## Пример

```csharp

GeneratorCrossword crossWord = new GeneratorCrossword(words, sizeX, sizeY);

crossWord.Generate();

char?[,] Field = crossWord.Field;

if ( Directory.Exists("Результат") == false)
{
    Directory.CreateDirectory("Результат");
}

using ( StreamWriter streamOpen = new StreamWriter("Результат/Использованные_слова.txt") )
{
    foreach ( string item in crossWord.ListOfUseWords )
    {
        await streamOpen.WriteLineAsync(item);
    }
}

using ( StreamWriter streamOpen = new StreamWriter("Результат/Не_использованные_слова.txt") )
{
    foreach ( string item in crossWord.ListOfExcessWords )
    {
        await streamOpen.WriteLineAsync(item);
    }
}

using ( StreamWriter streamOpen = new StreamWriter(new FileStream("Результат/Кроссворд.csv", FileMode.Create), Encoding.Default) )
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
