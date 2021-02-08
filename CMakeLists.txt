project(CjkLibCpp)

cmake_minimum_required (VERSION 3.8)

# Fügen Sie der ausführbaren Datei dieses Projekts eine Quelle hinzu.

aux_source_directory(_private_tests_cpp TstSrcFiles)

SET(SrcFiles
	logicUtil/sequencer.cpp
	logicUtil/edgeDetector.cpp
)

SET(SrcHeaderFolders
	logicUtil
	commandLine
	dsp
	string
	testing
	containers
)


add_executable (CjkLibTest
	${TstSrcFiles}
	${SrcFiles}
)

target_include_directories(CjkLibTest
	PRIVATE ${_private_tests_cpp}
	PRIVATE ${SrcHeaderFolders}
)