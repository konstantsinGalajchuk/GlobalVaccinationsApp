using MillionTimesVaccinationsApp.Models;

namespace MillionTimesVaccinationsApp.Data
{
    public static class DbInitializer
    {
        private static Random rnd = new Random();

        private static string GetRandomReviewDescription(int wordsCount)
        {
            string[] positiveReview = { "improved", "helped", "protected", "prevented" };
            string[] negativeReview = { "caused", "led to", "provoked" };
            string[] sideEffects = { "mild fever", "headache", "fatigue", "muscle pain", "chills", "nausea", "diarrhea", "soreness at the injection site", "allergic reaction" };
            string review, effect;
            string description = string.Empty;

            for (int i = 0; i < wordsCount / 2; i++)
            {
                if (rnd.NextDouble() < 0.5)
                    review = positiveReview[rnd.Next(positiveReview.Length)];
                else
                    review = negativeReview[rnd.Next(negativeReview.Length)];

                effect = sideEffects[rnd.Next(sideEffects.Length)];
                description += review + " " + effect;

                if (i + 1 != wordsCount / 2)
                    description += ", ";
                else
                    description += ".";
            }

            return description;
        }

        private static DateTime GetRandomDate()
        {
            DateTime start = new DateTime(1995, 1, 1);
            DateTime end = new DateTime(2023, 1, 1);
            int range = (end - start).Days;

            return start.AddDays(rnd.Next(range));
        }

        private static string GetRandomRecommendation()
        {
            string[] recommendations = { "drink more water", "smile more", "exercise", "hit your head against the wall", " don't get any more vaccinations", "I have no idea what to do",
            "stand on your toes, clap your hands three times on the full moon and let out a long roar", "buy a coffin"};

            return recommendations[rnd.Next(recommendations.Length)];
        }

        private static (string, bool) GetRandomName()
        {
            string[] maleFirstNames = { "Ivan", "Pavel", "Sergei", "Dmitri", "Nikolai", "Vladimir", "Alexei", "Mikhail", "Andrei", "Boris" };
            string[] femaleFirstNames = { "Olga", "Tatiana", "Natalia", "Svetlana", "Irina", "Elena", "Anna", "Maria", "Ekaterina", "Anastasia" };
            string[] lastNames = { "Novoelev", "Staroklenov", "Cursiancarpov", "Raspberrin", "Chickenov", "Srednelipov", "Evtuchenkov", "Novodubov", "Chartov", "Kazubov" };
            string[] patronymics = { "Ivanovich", "Petrovich", "Sergeevich", "Dmitrievich", "Nikolaevich", "Vladimirovich", "Alexeevich", "Mikhailovich", "Andreevich", "Borisovich" };
            string firstName = "";
            string lastName = "";
            string patronymic = "";
            bool is_male;

            if (rnd.NextDouble() < 0.5)
            {
                firstName = maleFirstNames[rnd.Next(maleFirstNames.Length)];
                lastName = lastNames[rnd.Next(lastNames.Length)];
                patronymic = patronymics[rnd.Next(patronymics.Length)];
                is_male = true;
            }
            else
            {
                firstName = femaleFirstNames[rnd.Next(femaleFirstNames.Length)];
                lastName = lastNames[rnd.Next(lastNames.Length)] + "a";
                patronymic = patronymics[rnd.Next(patronymics.Length)].Replace("ich", "na");
                is_male = false;
            }
            return ($"{lastName} {firstName} {patronymic}", is_male);
        }

        private static string GetRandomDisease()
        {
            string[] diseases = new string[] { "Lactose intolerance syndrome", "Peanut allergy", "Flu", "Tonsillitis", "Pollen allergy", "Animal allergy", "Food allergy",
                "Drug allergy", "Dust allergy", "Honey allergy", "Bronchitis", "Asthma", "Diabetes", "Gastritis", "Stomach ulcer", "Pancreatitis", "Hepatitis", "Cirrhosis of the liver",
                "Oncology", "Diabetes mellitus", "Arthritis", "Osteochondrosis", "Osteoporosis", "Anemia", "Hemophilia", "Tuberculosis", "Pneumonia", "Bronchial asthma", "Alcoholism",
                "Drug addiction", "Mental disorders", "Down syndrome", "Asperger's syndrome", "Tourette's syndrome", "Klinefelter's syndrome", "Tourette's syndrome", "Klinefelter's syndrome",
                "Rett syndrome", "Alzheimer's syndrome", "Parkinson's syndrome", "Tourette's syndrome", "Klinefelter's syndrome" };

            return diseases[rnd.Next(diseases.Length)];
        }

        private static string GetRandomManufacturer()
        {
            string[] first = { "Innovative", "Incredible", "Mind-blowing", "Killer", "Outdated", "Useless", "Stupid", "Best" };
            string[] second = { "Guys", "Monsters", "Doctors", "Manufacturers", "Mediocrities", "Masons", "Dinosaurs" };
            string name;

            name = first[rnd.Next(first.Length)];
            name += second[rnd.Next(second.Length)];
            name += rnd.Next(101).ToString();

            return name;
        }

        private static string[] GetRandomAddress()
        {
            string[] regions = { "Brest", "Gomel", "Grodno", "Mogilev", "Minsk", "Vitebsk" };
            string[] brestCities = { "Brest", "Baranovichi", "Kobrin", "Luninets", "Pinsk", "Pruzhany" };
            string[] gomelCities = { "Gomel", "Mozir", "Rechitsa", "Zlobin", "Svetlogorsk", "Kalinkovitchi" };
            string[] grodnoCities = { "Grodno", "Lida", "Slonim", "Volkovisk", "Mosti", "Novogrudok" };
            string[] mogilevCities = { "Mogilev", "Bobruisk", "Osipovichi", "Gorki", "Mstislavl", "Klimovichi" };
            string[] minskCities = { "Minsk", "Saligorsk", "Molodechno", "Sluzk", "Dzerjinsk", "Borisov" };
            string[] vitebskCities = { "Vitebsk", "Orsha", "Polotsk", "Novopolotsk", "Glubokoe", "Postavi" };
            Dictionary<string, string[]> cities = new Dictionary<string, string[]>() {
                { regions[0], brestCities }, {regions[1], gomelCities}, {regions[2], grodnoCities },
                {regions[3], mogilevCities }, {regions[4], minskCities }, {regions[5],  vitebskCities }
            };
            string[] streets = { "Dzerjinskaja", "Oktiabria", "Pobedi", "Porajenya", "Lenina", "Pushkina", "Gastello", "Kaproney", "Dubovaja", "Agonii" };
            string[] address = new string[5];

            address[0] = regions[rnd.Next(regions.Length)];
            address[1] = cities[address[0]][rnd.Next(regions.Length)];
            address[2] = streets[rnd.Next(streets.Length)];
            address[3] = rnd.Next(100).ToString();
            address[4] = rnd.Next(50).ToString();

            return address;
        }

        private static string GetRandomPassport()
        {
            string[] letters = { "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "A", "S", "D", "F", "G", "H", "J", "K", "L", "Z", "X", "C", "V", "B", "N", "M" };
            string passport = letters[rnd.Next(letters.Length)];
            passport += letters[rnd.Next(letters.Length)];
            passport += rnd.Next(10).ToString();
            passport += rnd.Next(10).ToString();
            passport += rnd.Next(10).ToString();
            passport += rnd.Next(10).ToString();
            passport += rnd.Next(10).ToString();
            passport += rnd.Next(10).ToString();
            passport += rnd.Next(10).ToString();

            return passport;
        }

        public static void InitializeMessagesAfterVaccinations(int recordsCount, GlobalVaccinationsDbContext db)
        {
            string description;
            DateTime date;
            string recommendation;
            string doctor;

            for (int i = 0; i < recordsCount; i++)
            {
                description = GetRandomReviewDescription(rnd.Next(2, 11));
                date = GetRandomDate();
                recommendation = GetRandomRecommendation();
                (doctor, _) = GetRandomName();
                db.MessagesAfterVaccinations.Add(new MessagesAfterVaccination()
                {
                    Description = description,
                    Date = date,
                    Recommendations = recommendation,
                    Doctor = doctor
                });
            }

            db.SaveChanges();
        }

        public static void InitializeDoses(int recordsCount, GlobalVaccinationsDbContext db)
        {
            double value;

            for (int i = 0; i < recordsCount; i++)
            {
                value = rnd.NextDouble() * 10;
                db.Doses.Add(new Dose() { Value = value });
            }

            db.SaveChanges();
        }

        public static void InitializeDiseases(int recordsCount, GlobalVaccinationsDbContext db)
        {
            int code;
            string name;

            for (int i = 0; i < recordsCount; i++)
            {
                code = rnd.Next(100000);
                name = GetRandomDisease();
                db.Diseases.Add(new Disease() { Code = code, Name = name });
            }

            db.SaveChanges();
        }

        public static void InitializeVaccines(int recordsCount, GlobalVaccinationsDbContext db)
        {
            int diseaseId;
            string description;
            string manufacturer;

            int diseasesMax = db.Diseases.Max(d => d.DiseaseId) + 1;
            int diseasesMin = db.Diseases.Min(d => d.DiseaseId);

            for (int i = 0; i < recordsCount; i++)
            {
                diseaseId = rnd.Next(diseasesMin, diseasesMax);
                description = GetRandomReviewDescription(2);
                manufacturer = GetRandomManufacturer();
                db.Vaccines.Add(new Vaccine()
                {
                    DiseaseId = diseaseId,
                    Description = description,
                    Manufacturer = manufacturer
                });
            }

            db.SaveChanges();
        }

        public static void InitializeVaccineDoses(int recordsCount, GlobalVaccinationsDbContext db)
        {
            int doseId;
            int vaccineId;

            int maxDose = db.Doses.Max(d => d.DoseId) + 1;
            int minDose = db.Doses.Min(d => d.DoseId);
            int maxVaccine = db.Vaccines.Max(v => v.VaccineId) + 1;
            int minVaccine = db.Vaccines.Min(v => v.VaccineId);

            for (int i = 0; i < recordsCount; i++)
            {
                doseId = rnd.Next(minDose, maxDose);
                vaccineId = rnd.Next(minVaccine, maxVaccine);
                db.VaccineDoses.Add(new VaccineDose() { DoseId = doseId, VaccineId = vaccineId });
            }

            db.SaveChanges();
        }

        public static void InitializeMedicalInstitutions(int recordsCount, GlobalVaccinationsDbContext db)
        {
            string name;
            string[] address;

            for (int i = 0; i < recordsCount; i++)
            {
                name = GetRandomManufacturer();
                address = GetRandomAddress();
                db.MedicalInstitutions.Add(new MedicalInstitution()
                {
                    Name = name,
                    Region = address[0],
                    City = address[1],
                    Street = address[2],
                    HouseNumber = address[3],
                    ApartmentNumber = address[4]
                });
            }

            db.SaveChanges();
        }

        public static void InitializePatients(int recordsCount, GlobalVaccinationsDbContext db)
        {
            string name;
            bool is_male;
            string[] address;
            string sex;
            string passport;

            for (int i = 0; i < recordsCount; i++)
            {
                (name, is_male) = GetRandomName();
                sex = is_male ? "male" : "female";
                address = GetRandomAddress();
                passport = GetRandomPassport();
                db.Patients.Add(new Patient()
                {
                    FullName = name,
                    Sex = sex,
                    Passport = passport,
                    Region = address[0],
                    City = address[1],
                    Street = address[2],
                    HouseNumber = address[3],
                    ApartmentNumber = address[4]
                });

                db.SaveChanges();
            }
        }

        public static void InitializeVaccinations(int recordsCount, GlobalVaccinationsDbContext db)
        {
            int vaccineId;
            DateTime date;
            int doseNumber;
            int patientId;
            int medicalInstitutionId;

            int maxVaccine = db.Vaccines.Max(v => v.VaccineId) + 1;
            int minVaccine = db.Vaccines.Min(v => v.VaccineId);
            int maxPatient = db.Patients.Max(p => p.PatientId) + 1;
            int minPatient = db.Patients.Min(p => p.PatientId);
            int maxinstitution = db.MedicalInstitutions.Max(m => m.MedicalInstitutionId) + 1;
            int minInstitution = db.MedicalInstitutions.Min(m => m.MedicalInstitutionId);

            for (int i = 0; i < recordsCount; i++)
            {
                vaccineId = rnd.Next(minVaccine, maxVaccine);
                patientId = rnd.Next(minPatient, maxPatient);
                medicalInstitutionId = rnd.Next(minInstitution, maxinstitution);
                date = GetRandomDate();
                doseNumber = rnd.Next();
                db.Vaccinations.Add(new Vaccination
                {
                    VaccineId = vaccineId,
                    PatientId = patientId,
                    MedicalInstitutionId = medicalInstitutionId,
                    Date = date,
                    DoseNumber = doseNumber
                });
            }

            db.SaveChanges();
        }

        public static void UseDefaultInitialization(GlobalVaccinationsDbContext db)
        {
            InitializeMessagesAfterVaccinations(500, db);
            InitializeDoses(500, db);
            InitializeDiseases(500, db);
            InitializeVaccines(20000, db);
            InitializeVaccineDoses(20000, db);
            InitializeMedicalInstitutions(500, db);
            InitializePatients(500, db);
            InitializeVaccinations(20000, db);
        }
    }
}
