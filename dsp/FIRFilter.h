#pragma once

template<typename T, size_t Length>
class FIRFilter
{
	const T(&m_factors)[Length];
	T m_signal[Length];

public:
	FIRFilter(const T(&factors)[Length])
		: m_factors(factors)
		, m_signal{ 0 }
	{
	}

	T ProcessSample(const T input)
	{
		//Update Signal History
		for (size_t i = Length - 1; i > 0; i--)
		{
			m_signal[i] = m_signal[i - 1];
		}
		m_signal[0] = input;

		//Calculate CurrentOutput
		T result = T(0);
		for (size_t i = 0; i < Length; i++)
		{
			result += (m_signal[i] * m_factors[i]);
		}

		return result;
	}

};

