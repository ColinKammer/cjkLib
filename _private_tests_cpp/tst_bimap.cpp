#pragma once
#include "unique_bimap.h"

#include "test_helper.h"
#include <iostream>
#include <algorithm>

using namespace cjk;

int tst_bimap()
{
    int errors = 0;

    UniqueBimap<int, int> tstMap;

    incrementIfFalse(errors, doCheck("insertNew_1", tstMap.Insert(1, 101), "Unexpected Result"));
    incrementIfFalse(errors, doCheck("insertNew_2", tstMap.Insert(2, 102), "Unexpected Result"));
    incrementIfFalse(errors, doCheck("insertNew_3", tstMap.Insert(3, 103), "Unexpected Result"));
    incrementIfFalse(errors, doCheck("insertNew_4", tstMap.Insert(4, 104), "Unexpected Result"));
    incrementIfFalse(errors, doCheck("insertNew_5", tstMap.Insert(5, 105), "Unexpected Result"));

    incrementIfFalse(errors, doCheck("insertExisting_first", tstMap.Insert(5, 999) == false, "Unexpected Result"));
    incrementIfFalse(errors, doCheck("insertExisting_second", tstMap.Insert(99, 105) == false, "Unexpected Result"));

    incrementIfFalse(errors, doCheck("EntryCount", tstMap.GetEntries().size() == 5, "Unexpected EntryCount"));

    incrementIfFalse(errors, doCheck("getEntryByFirst_firstCorrect", (tstMap.FindEntryByFirst(1)->first == 1), "Unexpected Result"));
    incrementIfFalse(errors, doCheck("getEntryByFirst_secondtCorrect", (tstMap.FindEntryByFirst(1)->second == 101), "Unexpected Result"));
    incrementIfFalse(errors, doCheck("getEntryByFirst_unavailable", (tstMap.FindEntryByFirst(7) == nullptr), "Unexpected Result"));

    incrementIfFalse(errors, doCheck("removeResult", tstMap.Remove(tstMap.FindEntryByFirst(2)).first == 2, "Unexpected Result"));
    incrementIfFalse(errors, doCheck("CountAfterRemove", tstMap.GetEntries().size() == 4, "Unexpected Result"));

    return errors;
}