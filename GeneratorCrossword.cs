using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordGenerator
{
    class GeneratorCrossword
    {
        public readonly char?[,] Field;

        private List<string> _ListOfWords;
        public readonly List<string> ListOfExcessWords;
        public readonly List<string> ListOfUseWords;

        public GeneratorCrossword(string[] listOfWords, int sizeX, int sizeY)
        {
            Field = new char?[sizeX, sizeY];
            _ListOfWords = listOfWords.ToList<string>();
            ListOfExcessWords = new List<string>();
            ListOfUseWords = new List<string>();
        }

        public void Generate()
        {
            string word = _ListOfWords.ElementAt(0);
            _ListOfWords.RemoveAt(0);

            ListOfUseWords.Add(word);

            for ( int i = 0; i < word.Length; i++ )
            {
                Field[(Field.GetLength(0) / 2) + i, (Field.GetLength(0) / 2)] = word[i];
            }

            while ( true )
            {
                word = _ListOfWords.ElementAt(0);
                _ListOfWords.RemoveAt(0);

                try
                {
                    for ( int x = 0; x < Field.GetLength(0); x++ )
                    {
                        for ( int y = 0; y < Field.GetLength(1); y++ )
                        {
                            for ( int indexChar = 0; indexChar < word.Length; indexChar++ )
                            {
                                if ( Field[x, y] != word[indexChar] )
                                {
                                    continue;
                                }

                                if ( ListOfUseWords.Contains(word) )
                                {
                                    break;
                                }

                                if ( ((FieldHelper.IsNullTop(Field, x, y) && FieldHelper.IsNotNullBottom(Field, x, y)) ||
                                    (FieldHelper.IsNotNullTop(Field, x, y) && FieldHelper.IsNotNullBottom(Field, x, y)) ||
                                    (FieldHelper.IsNotNullTop(Field, x, y) && FieldHelper.IsNullBottom(Field, x, y))) &&
                                    (FieldHelper.IsNullLeft(Field, x, y) && FieldHelper.IsNullRigth(Field, x, y)) )
                                {
                                    bool error = false;

                                    for ( int i = 0; i < word.Length + 1; i++ )
                                    {
                                        if ( i == indexChar )
                                        {
                                            continue;
                                        }

                                        if ( FieldHelper.IsNotNullCell(Field, x - indexChar - 1, y) ||
                                             FieldHelper.IsNotNullTop(Field, x - indexChar - 1, y) ||
                                             FieldHelper.IsNotNullBottom(Field, x - indexChar - 1, y) )
                                        {
                                            error = true;
                                        }

                                        if ( FieldHelper.IsNotNullCell(Field, x - indexChar + i, y) ||
                                             FieldHelper.IsNotNullTop(Field, x - indexChar + i, y) ||
                                             FieldHelper.IsNotNullBottom(Field, x - indexChar + i, y) )
                                        {
                                            error = true;
                                        }
                                    }

                                    if ( error )
                                    {
                                        continue;
                                    }

                                    for ( int i = 0; i < word.Length; i++ )
                                    {
                                        Field[x - indexChar + i, y] = word[i];
                                    }

                                    ListOfUseWords.Add(word);

                                    break;
                                }

                                if ( ((FieldHelper.IsNullLeft(Field, x, y) && FieldHelper.IsNotNullRigth(Field, x, y)) ||
                                    (FieldHelper.IsNotNullLeft(Field, x, y) && FieldHelper.IsNotNullRigth(Field, x, y)) ||
                                    (FieldHelper.IsNotNullLeft(Field, x, y) && FieldHelper.IsNullRigth(Field, x, y))) &&
                                    (FieldHelper.IsNullTop(Field, x, y) && FieldHelper.IsNullBottom(Field, x, y)) )
                                {
                                    bool error = false;

                                    for ( int i = 0; i < word.Length + 1; i++ )
                                    {
                                        if ( i == indexChar )
                                        {
                                            continue;
                                        }

                                        if ( FieldHelper.IsNotNullCell(Field, x, y - indexChar - 1) ||
                                             FieldHelper.IsNotNullLeft(Field, x, y - indexChar - 1) ||
                                             FieldHelper.IsNotNullRigth(Field, x, y - indexChar - 1) )
                                        {
                                            error = true;
                                        }

                                        if ( FieldHelper.IsNotNullCell(Field, x, y - indexChar + i) ||
                                             FieldHelper.IsNotNullLeft(Field, x, y - indexChar + i) ||
                                             FieldHelper.IsNotNullRigth(Field, x, y - indexChar + i) )
                                        {
                                            error = true;
                                        }
                                    }

                                    if ( error )
                                    {
                                        continue;
                                    }

                                    for ( int i = 0; i < word.Length; i++ )
                                    {
                                        Field[x, y - indexChar + i] = word[i];
                                    }

                                    ListOfUseWords.Add(word);

                                    break;
                                }
                            }
                        }
                    }
                }
                catch ( IndexOutOfRangeException )
                {
                    ListOfExcessWords.Add(word);
                }

                if(_ListOfWords.Count == 0 )
                {
                    break;
                }
            }
        }

        class FieldHelper
        {
            public static bool IsNullTop(char?[,] field, int x, int y)
            {
                return field[x, y - 1] == null;
            }

            public static bool IsNullLeft(char?[,] field, int x, int y)
            {
                return field[x - 1, y] == null;
            }

            public static bool IsNullRigth(char?[,] field, int x, int y)
            {
                return field[x + 1, y] == null;
            }

            public static bool IsNullBottom(char?[,] field, int x, int y)
            {
                return field[x, y + 1] == null;
            }

            public static bool IsNullCell(char?[,] field, int x, int y)
            {
                return field[x, y] == null;
            }

            public static bool IsNotNullCell(char?[,] field, int x, int y)
            {
                return !(IsNullCell(field, x, y));
            }

            public static bool IsNotNullTop(char?[,] field, int x, int y)
            {
                return !(IsNullTop(field, x, y));
            }

            public static bool IsNotNullLeft(char?[,] field, int x, int y)
            {
                return !(IsNullLeft(field, x, y));
            }

            public static bool IsNotNullRigth(char?[,] field, int x, int y)
            {
                return !(IsNullRigth(field, x, y));
            }

            public static bool IsNotNullBottom(char?[,] field, int x, int y)
            {
                return !(IsNullBottom(field, x, y));
            }
        }
    }
}
