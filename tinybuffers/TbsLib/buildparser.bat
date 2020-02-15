echo Running Antlr Code Generation
antlrv4 -o generated -Dlanguage=CSharp -package AntlrTbsParser -no-listener -visitor TbsDefinition.g4