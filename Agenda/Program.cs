using System.IO;
using System.Text.RegularExpressions;

namespace Agenda
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char opcio;

            Console.WriteLine(ValidarCorreu("roger.pallkad@gmail.com"));
            Console.WriteLine(ValidarDni("41647574W"));
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
            StreamReader sR;
            sR = new StreamReader(@".\agenda.txt");
        }
        static void EscriureFitxer()
        {
            StreamWriter sW;
            sW = new StreamWriter(@".\agenda.txt");
        }

        // Mètode DonarAlta
        static void DonarAlta()
        {
            //Roger;Palmada;41673250B;633556238;17/04/2001;roger.palmada@gmail.com
            StreamWriter sW;
            sW = new StreamWriter(@".\agenda.txt");

            string nom, cognom, dni, telefon, dNaixa, correu, dades;
            Console.Write("Introdueix el teu nom: ");
            nom = EscriureNom(Console.ReadLine());
            nom = EscriureNom(nom);
            Console.Write("\nIntrodueix el teu cognom: ");
            cognom = EscriureNom(Console.ReadLine());
            cognom = EscriureNom(cognom);
            Console.Write("\nIntrodueix el teu DNI ");
            dni = Console.ReadLine();
            Console.Write("\nIntrodueix el teu número de telèfon: ");
            telefon = Console.ReadLine();
            Console.Write("\nIntrodueix la teva data de naixament: ");
            dNaixa = Console.ReadLine();
            Console.Write("\nIntrodueix el teu correu electrònic: ");
            correu = Console.ReadLine();
            
            // Validar Dades



            // Escriure les Dades
            dades = nom + ';' + cognom + ';' + dni + ';' + telefon + ';' + dNaixa + ';' + correu;
            sW.WriteLine(dades);
            sW.Close();
        }
        // Mètode validar DNI
        static bool ValidarDni(string dni)
        {
            bool valid = false;
            Regex patro = new Regex("^[1-9][0-9]{7}[A-Z]$");
            //Declaracio variables
            string dniNumeros = dni.Substring(0, 8);
            string dniLletra = dni.Substring(8, 1);
            string dniLletraReal = CalcularDNILletra(Convert.ToInt32(dniNumeros));

            //Comparació de les dos lletres del DNI
            if (patro.IsMatch(dni) && dniLletraReal == dniLletra)
            { 
                valid = true;   
            }
            return valid;
        }

               
        static string CalcularDNILletra(int dniNumeros)
            {
                string[] control = { "T", "R", "W", "A", "G", "M", "Y", "F", "P", "D", "X", "B", "N", "J", "Z", "S", "Q", "V", "H", "L", "C", "K", "E" };
                int residu = dniNumeros % 23;
                return control[residu];
            }

            // Mètode validar telèfon
            static bool ValidarTelefon(string telefon)
        {
            bool valid = true;
            if (!"0123456789".Contains(telefon)) valid = false;
            if (telefon.Length != 9) valid = false;
            return valid;
        }

        // Mètode escriure Nom
        static string EscriureNom(string nom)
        {
            string nouNom;
            nouNom = nom.Substring(0,1).ToUpper() + nom.Substring(1).ToLower();
            return nouNom;
        }

        // Mètode correu electronic
        static bool ValidarCorreu(string correu)
        {
            bool valid = true;
            string abansArroba, domini;
            correu = correu.ToLower();
            abansArroba = correu.Substring(0, correu.IndexOf('@'));
            domini = correu.Substring(correu.IndexOf("@"));

            if (!"@".Contains(correu)) valid = false;
            if (abansArroba.Length < 3) valid = false;
            if (!"qwertyuiopasdfghjklñzxcvbnm0123456789.".Contains(abansArroba)) valid = false;     // Fer aquesta linia be
            if (".es".Contains(domini) || ".com".Contains(domini))
            {
                domini = domini.Remove(domini.IndexOf('.'));
            }
            else valid = false;
            if (domini.Length < 3 && !"qwertyuiopasdfghjklñzxcvbnm".Contains(domini)) valid = false;
            return valid;
        }

        // Mètode Validar Data
        static bool ValidarData(string data)
        {
            bool valid = true;
            DateTime dataTime;
            string dia, mes, any;
            int diaNum, mesNum, anyNum;
            // dd/mm/yyyy
            dia = data.Substring(0, 2);
            diaNum = Convert.ToInt32(dia);
            mes = data.Substring(3, 2);
            mesNum = Convert.ToInt32(mes);
            any = data.Substring(6);
            anyNum = Convert.ToInt32(any);


            if ((diaNum < 1 || diaNum > 31) && (mesNum < 1 || mesNum > 12))
            {
                valid = false;
            }
            else if ((diaNum < 1 || diaNum > 30) && (mesNum != 4 || mesNum != 6 || mesNum != 4 || mesNum != 9 || mesNum != 11))
            {
                valid = false;
            }
            else if ((diaNum < 1 || diaNum > 29) && mesNum != 2 && (anyNum % 4 == 0))
            {

            }
            else if ((diaNum < 1 || diaNum > 28) && mesNum != 2)
            {
                valid = false;
            }




                return valid;
        }






    }








}
