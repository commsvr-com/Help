systeminfo| find "Windows XP" > nul
if %ERRORLEVEL% == 0 goto WinXp

systeminfo | find "Windows 7" > nul
if %ERRORLEVEL% == 0 goto Win7

systeminfo | find "Windows Vista" > nul
if %ERRORLEVEL% == 0 goto WinVista

rem ---------------------------- for Windows XP --------------------------------
:WinXp
echo Installing on Windows XP
set ProgramData=%ALLUSERSPROFILE%\Application Data\
del /S /Q /F "%ProgramData%\EWSoftware\Sandcastle Help File Builder\Plug-Ins\*.*"
xcopy "\\Hq.cas.com.pl\cas_dfs\cas\Projekty\Produkty\EX02-MAML\EX0205-Deliverables\recent\*.*" "%ProgramData%\EWSoftware\Sandcastle Help File Builder\Components and Plug-Ins\" /S /R
goto FinalOperations
rem ----------------------------------------------------------------------------

rem ---------------------------For Windows 7 x64 -------------------------------
:Win7
echo Installing on Windows 7 x64
del /S /Q /F "%ProgramFiles(x86)%\EWSoftware\Sandcastle Help File Builder\Components and Plug-Ins\*.*"
xcopy "\\Hq.cas.com.pl\cas_dfs\cas\Projekty\Produkty\EX02-MAML\EX0205-Deliverables\recent\*.*" "%ProgramFiles(x86)%\EWSoftware\Sandcastle Help File Builder\Components and Plug-Ins\" /S /R
goto FinalOperations
rem ----------------------------------------------------------------------------

rem -------------------------- For Windows Vista -------------------------------
:WinVista
echo Installing on Windows Vista
del /S /Q /F "%ProgramData%\EWSoftware\Sandcastle Help File Builder\Plug-Ins\*.*"
xcopy "\\Hq.cas.com.pl\cas_dfs\cas\Projekty\Produkty\EX02-MAML\EX0205-Deliverables\recent\*.*" "%ProgramData%\EWSoftware\Sandcastle Help File Builder\Components and Plug-Ins\" /S /R
goto FinalOperations
rem ----------------------------------------------------------------------------

:FinalOperations
rem Install WEB files
cd %~dp0%
xcopy ".\EWSoftware\Web\*.*" "%SHFBROOT%Web"\ /E /R /Q /Y
rem Install Presentations
xcopy ".\Sandcastle\Presentation\*.*" "%DXROOT%Presentation\" /E /R /Q /Y

pause > nul
