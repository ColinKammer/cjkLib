#pragma once

#define CALL_EVERY(X, VARIABLENAME, CODE) { \
    static auto VARIABLENAME = X ;\
    VARIABLENAME--;\
\
    if(VARIABLENAME <= 0)\
	{\
        { CODE }\
        VARIABLENAME = X ;\
    }\
}
