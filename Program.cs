using System;
using System.Linq;
using System.Text;

class JogoDaForca
{
    static void Main()
    {
        while (true)
        {
            MostrarMenu();
            int opcao = LerInteiro("Escolha uma opção: ");

            switch (opcao)
            {
                case 1:
                    Console.Clear();
                    MostrarMenuTemas();
                    int tema = LerInteiro("Escolha um tema: ");

                    string[] palavras = null;
                    string dica = "";

                    switch (tema)
                    {
                        case 1:
                            palavras = new[] { "Galinha", "Mosquito", "Javali", "Gorila", "Cavalo" };
                            dica = "É um animal.";
                            break;
                        case 2:
                            palavras = new[] { "Mesa", "Cadeira", "Computador", "Lâmpada", "Caneta" };
                            dica = "É um objeto.";
                            break;
                        case 3:
                            palavras = new[] { "Vermelho", "Azul", "Verde", "Amarelo", "Roxo" };
                            dica = "É uma cor.";
                            break;
                        case 4:
                            Console.Clear();
                            continue;
                        case 5:
                          
                            Console.Write("Digite a palavra: ");
                            string palavraManual = LerPalavraOculta().ToUpper();
                            Console.Write("Digite uma dica sobre a palavra: ");
                            dica = Console.ReadLine();
                            Jogar(palavraManual, dica);
                            continue;
                        default:
                            Console.WriteLine("Opção inválida!");
                            continue;
                    }

                    string palavraEscolhida = palavras[new Random().Next(palavras.Length)].ToUpper();
                    Jogar(palavraEscolhida, dica);
                    break;

                case 2:
                    Console.WriteLine("Saindo do jogo...");
                    return;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
    }

    static void MostrarMenu()
    {
        Console.WriteLine("* Jogo da Forca *");
        Console.WriteLine("1 - Jogar");
        Console.WriteLine("2 - Sair");
    }

    static void MostrarMenuTemas()
    {
        Console.WriteLine("Escolha um tema:");
        Console.WriteLine("1 - Animais");
        Console.WriteLine("2 - Objetos");
        Console.WriteLine("3 - Cores");
        Console.WriteLine("4 - Voltar ao menu principal");
        Console.WriteLine("5 - Palavra personalizada (modo oculto)");
    }

    static void Jogar(string palavraSecreta, string dica)
    {
        char[] letrasDescobertas = new string('_', palavraSecreta.Length).ToCharArray();
        int tentativasRestantes = 7;
        string letrasTentadas = "";

        while (tentativasRestantes > 0 && new string(letrasDescobertas) != palavraSecreta)
        {
            Console.Clear();
            Console.WriteLine("* Jogo da Forca *");
            Console.WriteLine($"Dica: {dica}");
            Console.WriteLine($"Palavra: {string.Join(" ", letrasDescobertas)}");
            Console.WriteLine($"Tentativas restantes: {tentativasRestantes}");
            Console.WriteLine($"Letras tentadas: {letrasTentadas}");

            Console.Write("Digite uma letra: ");
            char tentativa;
            if (!char.TryParse(Console.ReadLine().ToUpper(), out tentativa) || !char.IsLetter(tentativa))
            {
                Console.WriteLine("Entrada inválida! Pressione enter para continuar...");
                Console.ReadKey();
                continue;
            }

            if (letrasTentadas.Contains(tentativa))
            {
                Console.WriteLine("Você já tentou essa letra. Tente outra!");
                Console.ReadKey();
                continue;
            }

            letrasTentadas += tentativa + " ";

            if (palavraSecreta.Contains(tentativa))
            {
                for (int i = 0; i < palavraSecreta.Length; i++)
                {
                    if (palavraSecreta[i] == tentativa)
                        letrasDescobertas[i] = tentativa;
                }
            }
            else
            {
                tentativasRestantes--;
            }
        }

        Console.Clear();
        Console.WriteLine("Fim do Jogo");
        Console.WriteLine($"Dica: {dica}");
        if (new string(letrasDescobertas) == palavraSecreta)
        {
            Console.WriteLine($"Parabéns, palavra correta: {palavraSecreta}");
        }
        else
        {
            Console.WriteLine($"Você perdeu! A palavra era: {palavraSecreta}");
        }

        Console.WriteLine("Pressione qualquer tecla para continuar.");
        Console.ReadKey();
    }

    static int LerInteiro(string mensagem)
    {
        int valor;
        while (true)
        {
            Console.Write(mensagem);
            if (int.TryParse(Console.ReadLine(), out valor))
                return valor;
            Console.WriteLine("Valor inválido. Tente novamente.");
        }
    }

    static string LerPalavraOculta()
    {
        StringBuilder sb = new StringBuilder();
        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(true);
            if (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Backspace)
            {
                sb.Append(key.KeyChar);
                Console.Write("*");
            }
            else if (key.Key == ConsoleKey.Backspace && sb.Length > 0)
            {
                sb.Length--;
                Console.Write("\b \b");
            }
        } while (key.Key != ConsoleKey.Enter);
        Console.WriteLine();
        return sb.ToString();
    }
}