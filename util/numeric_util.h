#pragma once

namespace cjk
{
    template<typename T>
    T roundUpToAllignment(T value, T allignment)
    {
        auto mod = value % allignment;
        if (mod == 0) return value;
        return value + allignment - mod;
    }
}
