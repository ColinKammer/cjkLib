# CMakeList.txt: CMake-Projekt für "CjkLib". Schließen Sie die Quelle ein, und definieren Sie
# projektspezifische Logik hier.
#
cmake_minimum_required (VERSION 3.8)

# Fügen Sie der ausführbaren Datei dieses Projekts eine Quelle hinzu.

aux_source_directory(test TstSrcFiles)

SET (TstIncludeDir
	"test/"
)

SET (HeaderFiles
	"public/include/"
)



add_executable (CjkLibTest ${TstSrcFiles})
target_include_directories(CjkLibTest
	PRIVATE ${TstHeaderFiles}
	PRIVATE ${HeaderFiles}
)