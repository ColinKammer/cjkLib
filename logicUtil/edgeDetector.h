#pragma once

#include <functional>
#include <vector>

namespace cjk {

	class EdgeDetector
	{
	private:
		bool m_lastState;
		const std::function<bool(void)> m_evalFunction;
	public:
		std::vector<std::function<void(void)>> m_risingEdgeDetected;
		std::vector<std::function<void(void)>> m_fallingEdgeDetected;

		EdgeDetector(const std::function<bool(void)> evalFunction);
		void service();
	};
}