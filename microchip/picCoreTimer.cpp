#include "picCoreTimer.h"

#include <xc.h>

#define CORETMR_FREQ (systemFrequency/2)

#define CORETMR_1US (CORETMR_FREQ / 1000000)


namespace cjk {
    namespace picCoreTimer {
        uint64_t g_elapsedUs = 0;

        void Service(uint32_t systemFrequency) { //e.g. 80Mhz => 80000000         
            uint32_t elapsedAfterService_us = _CP0_GET_COUNT() / CORETMR_1US;
            g_elapsedUs += elapsedAfterService_us;

            uint32_t evaluatedCoreTmrCycles = elapsedAfterService_us * CORETMR_1US;

            __builtin_disable_interrupts();
            _CP0_SET_COUNT(_CP0_GET_COUNT() - evaluatedCoreTmrCycles); //will intruduce small error (few ticks / Service)
            __builtin_enable_interrupts();
        }

        uint64_t GetElapsedS() {
            return g_elapsedUs / (1000 * 1000);
        }

        uint64_t GetElapsedMs() {
            return g_elapsedUs / 1000;
        }

        uint64_t GetElapsedUs() {
            return g_elapsedUs;
        }
    }
}

