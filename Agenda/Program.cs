using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Agenda
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine(Agenda());
            TornarMenu();
            DonarAlta();
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
                "q - Sortir\n";
            return agenda;
        }

        // Mètode ValidarOpcio
        static bool ValidarOpcio(char opcio)
        {
            bool valid = false;
            if (opcio > '0' && opcio < '7' || opcio == 'q' || opcio == 'Q')
            {
                valid = true;
            }
            return valid;
        }

        // Mètode SeleccionarOpcio
        static void SeleccionarOpcio(char opcio)
        {
            Console.Clear();
            switch (opcio)
            {
                case '1':
                    DonarAlta();
                    break;
                case '2':
                    // Recuperar Usuari

                    break;
                case '3':
                    // Modificar Usuari

                    break;
                case '4':
                    // Eliminar Usuari

                    break;
                case '5':
                    //Mostrar Agenda

                    break;
                case '6':
                    // Ordenar Agenda

                    break;
                case 'q':
                    // Sortir

                    break;

            }
        }

        // Mètode TornarMenu
        static void TornarMenu()
        {
            Console.WriteLine($"\n\n-----------------------------------------");
            Console.WriteLine($"Prem qualsevol botó per tornar a l'agenda ...");
            char tornar = Console.ReadKey().KeyChar;
        }

        // Mètode DonarAlta
        static void DonarAlta()
        {
            //Roger;Palmada;41673250B;633556238;17/04/2001;roger.palmada@gmail.com
            StreamWriter sW;
            sW = new StreamWriter(new FileStream(@".\agenda.txt", FileMode.Append));

            string nom, cognom, dni, telefon, dNaixa, correu, dades;
            // Demana nom i cognom
            Console.Write("Introdueix el teu nom: ");
            nom = EscriureNom(Console.ReadLine());
            nom = EscriureNom(nom);

            Console.Write("Introdueix el teu cognom: ");
            cognom = EscriureNom(Console.ReadLine());
            cognom = EscriureNom(cognom);

            // Demana el DNI
            dni = DemanarDni();

            // Demana el telefon
            telefon = DemanarTelefon();

            // Demana la data de naixament
            dNaixa = DemanarData();

            // Demana el correu electronic
            correu = DemanarCorreu();
            
            // Escriure les Dades
            dades = nom + ';' + cognom + ';' + dni + ';' + telefon + ';' + dNaixa + ';' + correu;
            sW.WriteLine(dades);
            sW.Close();
        }

        // Mètode Demanar DNI
        static string DemanarDni()
        {
            string dni;

            Console.Write("Introdueix el teu número de DNI (format: '41673251B'): ");
            dni = Console.ReadLine();

            while (!ValidarDni(dni))
            {
                Console.Write("ERROR DE LECTURA: Torna a introduir el teu número de DNI (format: '41673251B'); ");
                dni = Console.ReadLine();
            }
            return dni;
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

        // Mètode Demanar Telefon
        static string DemanarTelefon()
        {
            string telefon;

            Console.Write("Introdueix el teu número de telèfon (format: '972265402'): ");
            telefon = Console.ReadLine();

            while (!ValidarTelefon(telefon))
            {
                Console.Write("ERROR DE LECTURA: Torna a introduir el teu número de telèfon (format: '972265402'): ");
                telefon = Console.ReadLine();
            }
            return telefon;
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
            bool valid = false;
            Regex patro = new Regex("^[1-9][0-9]{8}$");
            if (patro.IsMatch (telefon)) valid = true;
            return valid;
        }

        // Mètode Escriure Nom
        static string EscriureNom(string nom)
        {
            string nouNom;
            nouNom = nom.Substring(0,1).ToUpper() + nom.Substring(1).ToLower();
            return nouNom;
        }

        // Demanar Correu Electrònic
        static string DemanarCorreu()
        {
            string correu;

            Console.Write("Introdueix el teu correu electrònic: ");
            correu = Console.ReadLine();

            while (!ValidarCorreu(correu))
            {
                Console.Write("ERROR DE LECTURA: Torna a introduir el teu correu electrònic: ");
                correu = Console.ReadLine();
            }
            return correu;

        }

        // Mètode Validar Correu 
        static bool ValidarCorreu(string correu)
        {
            bool valid = false;
            Regex patro = new Regex(@"^[a-z0-9.]{3,}[@][a-z.]{3,}[/.](com||es)$");
            if (patro.IsMatch(correu)) valid = true;
            return valid;
        }

        // Demanar Data
        static string DemanarData()
        {
            string data;

            Console.Write("Introdueix la teva data de naixament (format: 'dd/mm/yyyy'): ");
            data = Console.ReadLine();

            while (!ValidarData(data))
            {
                Console.Write("ERROR DE LECTURA: Torna a introduir la teva data de naixament (format: 'dd/mm/yyyy'): ");
                data = Console.ReadLine();
            }
            return data;

        }

        // Mètode Validar Data
        static bool ValidarData(string dataString)
        {
            bool valid = false;
            DateTime dataTime;

            if (DateTime.TryParse(dataString, out dataTime))
            {
                valid = true;
            }
            return valid;
        }






    }








}
