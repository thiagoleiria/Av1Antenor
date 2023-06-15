Console.WriteLine("Digite uma sequência de simbolos");
string inputString = "";
while (inputString.Length == 0 || inputString.Length > 10) {
    inputString = Console.ReadLine();
    if (inputString.Length > 10) {
        Console.WriteLine("Entrada Não Permitida. Verifique sua entrada:");
    }
}

List<string> tokens = Tokenize(inputString);
Console.WriteLine("Tokens:");
foreach (string token in tokens) {
    Console.WriteLine(token);
}


    

    static List<string> Tokenize(string inputString) {
        HashSet<char> validChars = new HashSet<char>("abcdefghijklmnopqrsuvABCDEFGHIJKLMNOPQRSUV0123456789+-*/%()[]{}<>=!&|~^,$.@#?");
        HashSet<char> validOperators = new HashSet<char>("+-*/%<>=!&|~^");
        List<string> tokens = new List<string>();
        string buffer = "";


    if (Char.IsDigit(inputString[0])) {
    Console.WriteLine("Palavras iniciadas com números são sempre palavras reservadas pelo sistema.");
}


        for (int i = 0; i < tokens.Count; i++) {
            string token = tokens[i];
            if (Char.IsDigit(token[0])) {
                Console.WriteLine($"A palavra '{token}' é uma palavra indisponível.");
            }
        }

        for (int i = 0; i < inputString.Length; i++) {
            char c = inputString[i];
            if (char.IsDigit(c) && buffer.Length == 0) {
                tokens.Add("Número");
                while (char.IsDigit(c) && i < inputString.Length - 1) {
                    buffer += c;
                    i++;
                    c = inputString[i];
                }
                if (char.IsDigit(c)) {
                    buffer += c;
                }
                tokens.Add(buffer);
                buffer = "";
            } else if (validChars.Contains(c)) {
                if (buffer.Length > 0 && IsTokenWithXYZTW(buffer)) {
                    tokens.Add("Expressão matemática");
                }
                buffer += c;
            } else {
                if (buffer.Length > 0) {
                    tokens.Add(buffer);
                    buffer = "";
                }
            }
        }


        if (buffer.Length > 0) {
            tokens.Add(buffer);
        }

        return tokens;
    }

    static bool IsTokenWithXYZTW(string token) {
        HashSet<char> xyztwChars = new HashSet<char>("xyztw");
        HashSet<char> validOperators = new HashSet<char>("+-*/%<>=!&|~^");
        bool xyztwFlag = false;
        bool operatorFlag = false;
        for (int i = 0; i < token.Length; i++) {
            char c = token[i];
            if (xyztwChars.Contains(c)) {
                xyztwFlag = true;
                if (i < token.Length - 1) {
                    char nextChar = token[i + 1];
                    if (validOperators.Contains(nextChar)) {
                        operatorFlag = true;
                        i++;
                    } else {
                        return false;
                    }
                }
            } else if (char.IsDigit(c)) {
                return false;
            } else if (char.IsLetter(c)) {
                return false;
            } else if (validOperators.Contains(c)) {
                if (!xyztwFlag) {
                    return false;
                }
                if (operatorFlag) {
                    return false;
                }
                operatorFlag = true;
            } else {
                return false;
            }
        }
        return xyztwFlag && operatorFlag;
    }