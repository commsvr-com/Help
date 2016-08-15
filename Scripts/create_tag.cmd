rem//  $LastChangedDate: 2009-05-25 18:17:45 +0200 (Pn, 25 maj 2009) $
rem//  $Rev: 3082 $
rem//  $LastChangedBy: mzbrzezny $
rem//  $URL: svn://svnserver.hq.cas.com.pl/VS/trunk/PR32-ModelDesigner/Scripts/create_tag.cmd $
rem//  $Id: create_tag.cmd 3082 2009-05-25 16:17:45Z mzbrzezny $

if "%1"=="" goto ERROR

./create_branch %1 tags

goto EXIT
:ERROR
echo Parametr must be set
:EXIT
