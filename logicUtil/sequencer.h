#pragma once

#include <string>
#include <functional>

namespace cjk {

	class Sequencer
	{
		using SequenceDescription = std::string;
		using StepExecutor = std::function<void(char)>;

	public:
		SequenceDescription m_sequence = ""; //changes will take effect when the next step is executed

		Sequencer(const StepExecutor stepExecutor = nullptr);

		char ExecuteStep(); //returns the char sent to callback (for non sychron oriented programming)

	private:
		const StepExecutor m_stepExecutor;
		unsigned int m_nextStep = 0;
	};

}