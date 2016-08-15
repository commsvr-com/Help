rem//  $LastChangedDate: 2009-06-02 12:44:08 +0200 (Wt, 02 cze 2009) $
rem//  $Rev: 3147 $
rem//  $LastChangedBy: mzbrzezny $
rem//  $URL: svn://svnserver.hq.cas.com.pl/VS/trunk/PR32-ModelDesigner/Scripts/create_branch.cmd $
rem//  $Id: create_branch.cmd 3147 2009-06-02 10:44:08Z mzbrzezny $

if "%1"=="" goto ERROR
set branchtype=%2
if "%branchtype%"=="" goto setbranch

:dothejob
svn mkdir svn://svnserver.hq.cas.com.pl/VS/%branchtype%/HelpAssistant/%1  -m "created new %branchtype%  %1 (%branchtype% folder)"
svn copy svn://svnserver.hq.cas.com.pl/VS/trunk/EX02-MAML svn://svnserver.hq.cas.com.pl/VS/%branchtype%/HelpAssistant/%1/EX02-MAML -m "created new %branchtype% %1 (project EX02-MAML)"

goto EXIT

:setbranch
set branchtype=branches
goto dothejob
:ERROR
echo Parametr must be set
:EXIT
