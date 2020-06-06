#include "sequencer.h"

namespace cjk {

	Sequencer::Sequencer(const StepExecutor stepExecutor)
		: m_stepExecutor(stepExecutor)
	{

	}

	void Sequencer::ExecuteStep()
	{
		auto currentLenth = m_sequence.length();
		if (currentLenth == 0)
		{
			//empty sequence => nothing to do
			return;
		}

		if (m_nextStep >= currentLenth)
		{
			m_nextStep = 0;
		}
		m_stepExecutor(m_sequence[m_nextStep]);

		m_nextStep++;
	}
}