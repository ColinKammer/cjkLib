#pragma once

#include <cstdint>

namespace cjk {
    namespace picCoreTimer {
        void Service(uint32_t systemFrequency);

        uint64_t GetElapsedS();
        uint64_t GetElapsedMs();
        uint64_t GetElapsedUs();

    }
}


