#pragma once

#include <vector>

namespace cjk
{
    template<typename T>
    void push_back_vectorOnVector(std::vector<T>& destination, const std::vector<T>& source)
    {
        destination.insert(destination.end(), source.begin(), source.end());
    }
}
