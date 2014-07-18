k2-workflow
=================

开发中 git 使用流程 
-------------------
1. 发起流程
	start
	
2. 审批流程
	Approve
	
3. 加签
	Involve Task
	
4. 转签
	ReAssign Task
	
5. 获取任务项
	GetTaskList
	
6. 获取流程状态
	GetProcessStatus
	
7. 获取审批历史
	GetProcessComments
	

开发中 git 使用流程 
-------------------
1. 打开 ``git bash``，切换到常用开发目录

2. 首次获取项目：``git clone git@code.dianpingoa.com:ba-es/dianping-workflow.git``

3. ``cd  dianping-workflow``

4. 从当前分支 master 切换到 dev 分支：``git checkout dev``

5. 在当前分支 dev 下，新建 xx(自定义分支名，可使用同一前缀标识为特性分支) 分支：``git branch -b xx`` 或 ``git checkout -b xx``

6. 如果使用 ``git branch -b``创建的新分支需要切换分支到新建分支：``git checkout xx``

7. 当前位于 ``xx`` 分支，在该分支下开发相应的特性功能

8. 将工作区的改动添加到暂存区: ``git add -A`` 或 ``git add .`` 或 ``git add file``

9. 将暂存区的代码提交到当前分支：``git commit -m '注释'``

10. 当前分支的特性功能开发并自测完成，需推送当前特性分支到远端：``git push origin xx``

11. 若多人同时修改同一分支，在做第 ``10`` 步之前需要先获取远端当前分支最新代码，若发现冲突，需要自行解决，然后再执行第 ``10`` 步

12. 将当前分支推送到远端后，需要在 gitlab 上发起 merge request，选择将 ``xx`` 分支合并到 ``alpha`` 分支，并指定 CodeReview 的开发人员

13. CodeReview 完成后，同意将 ``xx`` 合并到 ``alpha``，并在 CI 上发布 ``alpha``

14. ``alpha`` 测试完成，需要 ``beta`` 测试时，在 gitlab 上发起 merge request，将 ``alpha`` 分支合并到 ``beta`` 分支，并指定开发人员解决冲突、合并、在 CI 上发布beta

15. 上线同上，``beta`` 到 ``master``，并 ``beta`` 到 ``dev``

16. 删除所有特性分支 ``xx``，删除 ``alpha``、``beta``分支

17. 从 ``dev`` 新建 ``alpha`` 和 ``beta`` 分支

18. 回到第 ``5`` 步

其他常用 git 命令
-----------------
- 查看远端分支：``git branch -r``

- 查看本地分支：``git branch``

- 撤销工作区的改动：``git checkout -- file`` 或 ``git checkout -- .``

- 删除暂存区的文件：``git rm --cached file``

- 撤销暂存区的改动：``git reset HEAD``

- 将某一文件撤出暂存区：``git reset -- file``

- 同时撤销工作区和暂存区的改动：``git checkout HEAD .`` 或 ``git checkout HEAD file``

- 比较工作区和暂存区：``git diff``

- 比较暂存区和HEAD：``git diff --cached``

- 比较工作区和HEAD：``git diff HEAD``

- 查看当前分支的状态：``git status``

- 查看branch分支username的提交日志：``git log branch --author username``

- ``gitk --all`` 或 ``gitk --since="2 weeks ago"``

- ``git tag``, ``git tag v0.0.1``

- ``git merge src-branch target-branch``

- ``git rebase``

- ``git fetch origin``

- ``git pull origin branch``

**实际流程可以和上面不一样，可根据项目来进行最优调整**