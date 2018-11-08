# VetEhrApp
PetLibrary containing shared classes, PetCollector to import pet data, PetAnalyzer to research stored pet data

Solution VetEhrApp contains the following projects:

  PetCollector:

    ASP .NET Core project that:
       - Imports pet data from a properly formatted CSV file. CSV file has no commas in data entries and contains, at minimum, the following columns: 'name', 'type', 'gender' (m/f), 'age'. Example CSV included.
       - Creates the file 'PetRepository.txt' (or replaces it) and stores the serialized pet data (will replace any existing data if re-imported at this time)

  PetAnalyzer:

    .Net Core console project that:
       - Retrieves serialized data from 'PetRepository.txt' (and deserializes it)
       - Analyzes the data to answer a few questions about the pets

   PetLibrary is a ClassLibrary that contains classes shared by the two projects.
