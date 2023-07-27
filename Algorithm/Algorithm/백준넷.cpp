#ifdef DFS와_BFS
#include <iostream>

#include <vector>
#include <queue>
#include <stack>
#include <algorithm>

using namespace std;

int main()
{
	int N, M, V;

	cin >> N >> M >> V;

	vector<vector<int>> v(N + 1, vector<int>());
	vector<bool> vc(N + 1, false);

	// 입력 부문
	for (int i = 0; i < M; i++)
	{
		int s, d;

		cin >> s >> d;

		v[s].push_back(d);
		v[d].push_back(s);
	}

	for (int i = 0; i < v.size(); i++)
	{
		sort(v[i].begin(), v[i].end(), less<int>());
		//cout << "[" << i << "]" << " : ";
		//for (auto ele : v[i])
		//	cout << ele << " ";
		//cout << endl;
	}

	stack<int> dfs;
	dfs.push(V);
	while (dfs.empty() != true)
	{
		auto top = dfs.top();
		dfs.pop();
		if (!vc[top])
		{
			vc[top] = true;
			cout << top << " ";
		}

		for (auto i : v[top])
		{
			if (vc[i] == false)
			{
				dfs.push(top);
				dfs.push(i);
				break;
			}
		}
	}
	cout << endl;

	queue<int> bfs;
	fill(vc.begin(), vc.end(), false);

	bfs.push(V);
	while (bfs.empty() != true)
	{
		auto front = bfs.front();
		bfs.pop();
		if (!vc[front])
		{
			vc[front] = true;
			cout << front << " ";
		}

		for (auto i : v[front])
		{
			if (vc[i] == false)
			{
				bfs.push(i);
			}
		}
	}

	return 0;
}


#endif // https://www.acmicpc.net/problem/1260
#ifdef 미로_탐색
#include <iostream>
#include <queue>
#include <vector>
#include <string>
#include <algorithm>

using namespace std;

int ns[4] = { 1, -1, 0, 0 };
int ew[4] = { 0, 0, 1, -1 };

int main()
{
	int N, M; // 세로, 가로

	//입력 부문
	cin >> N >> M;

	vector<vector<int>> v(N, vector<int>(M));
	for (int i = 0; i < N; i++)
	{
		for (int j = 0; j < M; j++)
			v[i][j] = -1;
	}

	cin.ignore();
	for (int i = 0; i < N; i++)
	{
		string str;
		cin >> str;
		for (int j = 0; j < M; j++)
		{
			if (str[j] == '1')
				v[i][j] = 0;
		}
	}

	queue<pair<int, int>> q;
	q.push({ 0, 0 });
	v[0][0] = 1;
	while (q.empty() != true)
	{
		auto curP = q.front();
		int curW = v[curP.first][curP.second];
		q.pop();
		// 동서남북 체크 걸음걸이 표시
		for (int i = 0; i < 4; i++)
		{
			pair<int, int> nextP = { curP.first + ns[i], curP.second + ew[i] };
			if (nextP.first >= 0 && nextP.first < N &&
				nextP.second >= 0 && nextP.second < M &&
				v[nextP.first][nextP.second] == 0)
			{
				q.push(nextP);
				v[nextP.first][nextP.second] = curW + 1;
			}
		}
	}

	//{
	//	for (auto i : v)
	//	{
	//		for (auto j : i)
	//			cout << j << '\t';
	//		cout << endl;
	//	}
	//	cout << endl;
	//}

	cout << v[N - 1][M - 1];

	return 0;
}

#endif // https://www.acmicpc.net/problem/2178
#ifdef 바이러스
#include <iostream>

#include <vector>
#include <stack>

using namespace std;



void DFS(int& count, int c, vector<bool>& vc, const vector<vector<int>>& v)
{
	for (auto i : v[c])
	{
		if (vc[i] == false)
		{
			vc[i] = true;
			count++;
			DFS(count, i, vc, v);
		}
	}
}

int main()
{
	int N, M;

	// 입력 부문
	cin >> N >> M;

	vector<bool> vc(N + 1);
	vector<vector<int>> v(N + 1, vector<int>());

	for (int i = 0; i < M; i++)
	{
		int s, d;
		cin >> s >> d;

		v[s].push_back(d);
		v[d].push_back(s);
	}
	int birus = 0;
	vc[1] = true;

	DFS(birus, 1, vc, v);


	/*
	stack<int> s;
	s.push(1);

	while (s.empty() != true)
	{
		auto c = s.top();
		s.pop();

		for (auto i : v[c])
		{
			if (vc[i] == false)
			{
				s.push(c);
				s.push(i);
				vc[i] = true;
				birus++;
			}
		}
	}
	*/

	cout << birus;
	return 0;
}
#endif // https://www.acmicpc.net/problem/2606
#ifdef 촌수계산
#include <iostream>
#include <vector>
#include <queue>

using namespace std;

int main()
{
	// 입력 부문
	int N, S, R, I;

	cin >> N >> S >> R >> I;

	vector<vector<int>> v(N + 1, vector<int>());
	vector<bool> vc(N + 1);

	for (int i = 0; i < I; i++)
	{
		int x, y;
		cin >> x >> y;

		v[x].push_back(y);
		v[y].push_back(x);
	}

	queue<int> q;

	q.push(S);
	vc[S] = true;
	int index = 0;

	while (q.empty() != true)
	{
		vector<int> tv;
		while (q.empty() != true)
		{
			auto front = q.front();
			if (front == R)
			{
				cout << index << endl;
				return 0;
			}
			q.pop();

			for (auto i : v[front])
			{
				if (vc[i] == false)
				{
					tv.push_back(i);
					vc[i] = true;
				}
			}
		}

		for (auto i : tv)
		{
			q.push(i);
		}
		index++;
	}

	index = -1;

	cout << index << endl;

	return 0;
}
#endif // https://www.acmicpc.net/workbook/view/1983
