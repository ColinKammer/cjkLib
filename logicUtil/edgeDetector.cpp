#include "edgeDetector.h"

namespace cjk {

	EdgeDetector::EdgeDetector(const std::function<bool(void)> evalFunction)
		: m_evalFunction(evalFunction)
	{
		m_lastState = evalFunction();
	}

	void EdgeDetector::service()
	{
		bool currentState = m_evalFunction();
		if ((currentState == true) && (m_lastState == false))
		{
			for (auto e : m_risingEdgeDetected)
				e();
		}
		else if ((currentState == false) && (m_lastState == true))
		{
			for (auto e : m_fallingEdgeDetected)
				e();
		}
		else
		{
			//no state change nothing to do
		}
		m_lastState = currentState;
	}
}