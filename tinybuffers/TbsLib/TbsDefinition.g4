grammar TbsDefinition;

@header {

}

file                : definition+
                    ;

definition          : enumdef										#EnumDefinition
                    | structdef										#StructDefinition
                    ;

enumdef             : 'enum' TYPENAME '{' enumoption+ '}'
                    ;
enumoption          : IDENTIFIER ';'
                    ;

structdef           : 'struct' TYPENAME '{' structmember+ '}'
                    ;

structmember        : TYPENAME IDENTIFIER ';'						#StructMemberScalar
                    | TYPENAME '[' POSNUMBER ']' IDENTIFIER ';'		#StructMemberArray
                    ;


IDENTIFIER          : [a-z] [a-zA-Z0-9]* ; //C-Style identiefiers, but have to start with lowerCase Letter
TYPENAME            : [A-Z] [a-zA-Z0-9]* ; //C-Style Typenames, but have to start with upperCase Letter
POSNUMBER           : [0-9]+ ;

WS                  : [ \t\r\n]+ -> skip ;