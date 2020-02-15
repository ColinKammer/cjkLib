// CjkLib.cpp: Definiert den Einstiegspunkt für die Anwendung.
//

#include "tstMain.h"

//Test Main function declarations
#define TSTDECL(TSTNAME) extern int tst_ ## TSTNAME ## ();
TESTLIST(TSTDECL)

int main()
{
	std::cout << "Starting Test... \n";
	
	int errorCount = 0;

	//log and Call Test Main Functions
#define TSTCALL(TSTNAME) {\
	std::cout << "\nTesting " << #TSTNAME << "...";\
	errorCount += tst_ ## TSTNAME ## ();\
}
	TESTLIST(TSTCALL);

	std::cout << "\n\n Test Finished with " << errorCount << " Errors\n";
	return 0;
}
