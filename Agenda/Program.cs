using System.IO;

namespace Agenda
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char opcio;

            Console.WriteLine(EscriureNom("AGIONEjdfioasf"));
        }



        // Mètode Agenda
        static string Agenda()
        {
            string agenda =
                "AGENDA\n" +
                "1 - Donar d'alta\n" +
                "2 - Recuperar Usuari\n" +
                "3 - Modificar usuari\n" +
                "4 - Eliminar Usuari\n" +
                "5 - Mostrar Agenda\n" +
                "6 - Ordenar Agenda\n" +
                "q - Sortir";
            return agenda;
        }
        
        // Mètode LLegirFitxer
        static void LlegirFitxer()
        {
            StreamReader agenda;
            agenda = new StreamReader(@".\agenda.txt");
        }
        static void EscriureFitxer()
        {
            StreamWriter escriureAgenda;
            escriureAgenda = new StreamWriter(@".\agenda.txt");
        }

        // Mètode DonarAlta
        static void DonarAlta()
        {
            EscriureFitxer();
            string nom, cognom, dni, telefon, dNaixa, correu;
            Console.Write("Introdueix el teu nom: ");
            nom = EscriureNom(Console.ReadLine());
            Console.Write("\nIntrodueix el teu cognom: ");
            cognom = EscriureNom(Console.ReadLine());
            Console.Write("\nIntrodueix el teu DNI ");
            dni = Console.ReadLine();
            Console.Write("\nIntrodueix el teu número de telèfon: ");
            telefon = Console.ReadLine();
            Console.Write("\nIntrodueix la teva data de naixament: ");
            dNaixa = Console.ReadLine();
            Console.Write("\nIntrodueix el teu correu electrònic: ");
            correu = Console.ReadLine();
        }

        static bool ValidarTelefon(string telefon)
        {
            bool valid = true;
            if (!"0123456789".Contains(telefon)) valid = false;
            if (telefon.Length != 9) valid = false;
            return valid;
        }
        static string EscriureNom(string nom)
        {
            string nouNom;
            nouNom = nom.Substring(0,1).ToUpper() + nom.Substring(1).ToLower();
            return nouNom;
        }
    }
}
