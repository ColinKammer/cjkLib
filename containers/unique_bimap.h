#pragma once
#include <vector>

namespace cjk {

    template<typename T1, typename T2>
    class UniqueBimap
    {
        using Entry = std::pair<T1, T2>;

    public:
        Entry* FindEntryByFirst(const T1& element)
        {
            for (Entry& entry : m_entries)
            {
                if (entry.first == element) return &entry;
            }
            return nullptr;
        }

        Entry* FindEntryBySecond(const T2& element)
        {
            for (Entry& entry : m_entries)
            {
                if (entry.second == element) return &entry;
            }
            return nullptr;
        }


        bool Insert(T1&& first, T2&& second)
        {
            if (FindEntryByFirst(first) != nullptr)
                //This Bimap is UNIQUE -> first already exists
                return false;

            if (FindEntryBySecond(second) != nullptr)
                //This Bimap is UNIQUE -> second already exists
                return false;

            m_entries.push_back(std::make_pair<T1, T2>(std::move(first), std::move(second)));
            return true;
        }

        Entry&& Remove(Entry* entry)
        {
            for (size_t i = 0; i < m_entries.size(); i++)
            {
                if (&m_entries[i] == entry) {
                    Entry entryToBeRemoved(m_entries[i]);
                    m_entries.erase(m_entries.begin() + i);
                    return std::move(entryToBeRemoved);
                }
            }

            return Entry();
        }

        const std::vector<Entry>& GetEntries()
        {
            return m_entries;
        }

    private:
        std::vector<Entry> m_entries;
    };

}