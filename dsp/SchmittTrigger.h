#pragma once

template<typename T>
class SchmittTrigger
{
	bool m_state;
	const T m_lowerThreshold;
	const T m_upperThreshold;

public:
	SchmittTrigger(const T lowerThreshold, const T upperThreshold, const bool initialState)
		: m_lowerThreshold(lowerThreshold)
		, m_upperThreshold(upperThreshold)
		, m_state(initialState)
	{
	}

	bool ProcessSample(const T input)
	{
		if (m_state)
		{
			if (input < m_lowerThreshold) m_state = false;
		}
		else
		{
			if (input > m_upperThreshold) m_state = true;
		}

		return m_state;
	}

};

