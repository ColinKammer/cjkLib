#include "sequencer.h"

namespace cjk {

	Sequencer::Sequencer(const StepExecutor stepExecutor)
		: m_stepExecutor(stepExecutor)
	{

	}

	char Sequencer::ExecuteStep()
	{
		auto currentLenth = m_sequence.length();
		if (currentLenth == 0)
		{
			//empty sequence => nothing to do
			return '\0';
		}

		if (m_nextStep >= currentLenth)
		{
			m_nextStep = 0;
		}
		auto currentStepValue = m_sequence[m_nextStep];
		m_nextStep++;

		if (m_stepExecutor != nullptr)
		{
			m_stepExecutor(currentStepValue);
		}
		return currentStepValue;
	}
}