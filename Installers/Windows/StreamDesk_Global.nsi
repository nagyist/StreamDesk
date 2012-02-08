!include "StreamDesk_Generated.nsh"

!define PRODUCT_NAME "NasuTek StreamDesk"
!define PRODUCT_PUBLISHER "NasuTek Enterprises"
!define PRODUCT_WEB_SITE "http://www.streamdesk.ca"
!define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\StreamDesk.exe"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"
!define PRODUCT_STARTMENU_REGVAL "NSIS:StartMenuDir"

SetCompressor lzma

; MUI 1.67 compatible ------
!include "MUI.nsh"

; MUI Settings
!define MUI_ABORTWARNING
!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\modern-install.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"

; Welcome page
!insertmacro MUI_PAGE_WELCOME
; License page
!insertmacro MUI_PAGE_LICENSE "..\..\LICENSE"
; Components page
!insertmacro MUI_PAGE_COMPONENTS
; Directory page
!insertmacro MUI_PAGE_DIRECTORY
; Start menu page
var ICONS_GROUP
!define MUI_STARTMENUPAGE_NODISABLE
!define MUI_STARTMENUPAGE_DEFAULTFOLDER "NasuTek StreamDesk"
!define MUI_STARTMENUPAGE_REGISTRY_ROOT "${PRODUCT_UNINST_ROOT_KEY}"
!define MUI_STARTMENUPAGE_REGISTRY_KEY "${PRODUCT_UNINST_KEY}"
!define MUI_STARTMENUPAGE_REGISTRY_VALUENAME "${PRODUCT_STARTMENU_REGVAL}"
!insertmacro MUI_PAGE_STARTMENU Application $ICONS_GROUP
; Instfiles page
!insertmacro MUI_PAGE_INSTFILES
; Finish page
!define MUI_FINISHPAGE_RUN "$INSTDIR\StreamDesk.exe"
!insertmacro MUI_PAGE_FINISH

; Uninstaller pages
!insertmacro MUI_UNPAGE_INSTFILES

; Language files
!insertmacro MUI_LANGUAGE "English"

; Reserve files
!insertmacro MUI_RESERVEFILE_INSTALLOPTIONS

; MUI end ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "StreamDesk-${PRODUCT_VERSION}.exe"
InstallDir "$PROGRAMFILES\NasuTek StreamDesk"
InstallDirRegKey HKLM "${PRODUCT_DIR_REGKEY}" ""
ShowInstDetails show
ShowUnInstDetails show

Section -Updater
  SetOutPath "$INSTDIR"
  
  WriteRegStr HKLM "Software\NasuTek Enterprises\Updater\InstalledProducts\NasuTek StreamDesk" "InstallPath" "$INSTDIR"
  WriteRegStr HKLM "Software\NasuTek Enterprises\Updater\DownloadRepositories" "StreamDesk_Repository" "http://streamdesk.sf.net/updater.xml"
SectionEnd

Section -SharedFeatures
  SetOutPath "$INSTDIR"
  
  WriteRegStr HKLM "Software\NasuTek Enterprises\Updater\InstalledProducts\NasuTek StreamDesk" "StreamDeskSharedComponents" "${PRODUCT_VERSION}"
  
  NSISdl::download "http://downloads.sourceforge.net/project/streamdesk/Unstable/3.0/Shared.7z" "Shared.7z"
  Nsis7z::Extract "Shared.7z"

  Delete "$OUTDIR\Shared.7z"
SectionEnd

Section "StreamDesk Core" SEC01
  SetOutPath "$INSTDIR"
  
  WriteRegStr HKLM "Software\NasuTek Enterprises\Updater\InstalledProducts\NasuTek StreamDesk" "StreamDeskCore" "${PRODUCT_VERSION}"
  
  NSISdl::download "http://downloads.sourceforge.net/project/streamdesk/Unstable/3.0/Client.7z" "Client.7z"
  Nsis7z::Extract "Client.7z"

  Delete "$OUTDIR\Client.7z"
  
  !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
  CreateDirectory "$SMPROGRAMS\$ICONS_GROUP"
  CreateShortCut "$SMPROGRAMS\$ICONS_GROUP\NasuTek StreamDesk.lnk" "$INSTDIR\StreamDesk.exe"
  CreateShortCut "$DESKTOP\NasuTek StreamDesk.lnk" "$INSTDIR\StreamDesk.exe"
  !insertmacro MUI_STARTMENU_WRITE_END
SectionEnd

Section "StreamDesk Editor" SEC02
  SetOutPath "$INSTDIR"

    WriteRegStr HKLM "Software\NasuTek Enterprises\Updater\InstalledProducts\NasuTek StreamDesk" "StreamDeskEditor" "${PRODUCT_VERSION}"
  
  NSISdl::download "http://downloads.sourceforge.net/project/streamdesk/Unstable/3.0/Editor.7z" "Editor.7z"
  Nsis7z::Extract "Editor.7z"
    
  Delete "$OUTDIR\Editor.7z"
  !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
  CreateShortCut "$SMPROGRAMS\$ICONS_GROUP\NasuTek StreamDesk Editor.lnk" "$INSTDIR\Editor.exe"
  !insertmacro MUI_STARTMENU_WRITE_END
SectionEnd

Section -AdditionalIcons
  SetOutPath $INSTDIR
  !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
  WriteIniStr "$INSTDIR\${PRODUCT_NAME}.url" "InternetShortcut" "URL" "${PRODUCT_WEB_SITE}"
  CreateShortCut "$SMPROGRAMS\$ICONS_GROUP\Website.lnk" "$INSTDIR\${PRODUCT_NAME}.url"
  CreateShortCut "$SMPROGRAMS\$ICONS_GROUP\Uninstall.lnk" "$INSTDIR\uninst.exe"
  !insertmacro MUI_STARTMENU_WRITE_END
SectionEnd

Section -Post
  WriteUninstaller "$INSTDIR\uninst.exe"
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "" "$INSTDIR\StreamDesk.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\StreamDesk.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
SectionEnd

; Section descriptions
!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
  !insertmacro MUI_DESCRIPTION_TEXT ${SEC01} "The Core of StreamDesk. This must be installed."
  !insertmacro MUI_DESCRIPTION_TEXT ${SEC02} "The editor of StreamDesk to create databases with."
!insertmacro MUI_FUNCTION_DESCRIPTION_END


Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "$(^Name) was successfully removed from your computer."
FunctionEnd

Function un.onInit
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "Are you sure you want to completely remove $(^Name) and all of its components?" IDYES +2
  Abort
FunctionEnd

Section Uninstall
  !insertmacro MUI_STARTMENU_GETFOLDER "Application" $ICONS_GROUP
  Delete "$DESKTOP\NasuTek StreamDesk.lnk"

  RMDir /r "$SMPROGRAMS\$ICONS_GROUP"
  RMDir /r "$INSTDIR"

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  DeleteRegKey HKLM "Software\NasuTek Enterprises\Updater\InstalledProducts\NasuTek StreamDesk"
  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"
  SetAutoClose true
SectionEnd