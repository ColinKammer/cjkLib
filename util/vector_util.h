#pragma once

#include <vector>

namespace cjk
{
    template<typename T>
    void push_back_vectorOnVector(std::vector<T>& destination, const std::vector<T>& source)
    {
        destination.insert(destination.end(), source.begin(), source.end());
    }

    template<typename T>
    std::vector<T*> createPointerVector(std::vector<T>& pointedToObjects)
    {
        std::vector<T*> retVal;
        retVal.reserve(pointedToObjects.size());
        for(auto& src : pointedToObjects)
            retVal.push_back(&src);
        return retVal;
    }
}
