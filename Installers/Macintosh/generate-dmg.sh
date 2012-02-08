# Remove old build files
rm -rf "StreamDesk.sparseimage" "StreamDesk.dmg"

# Unzip DMG Template
unzip "StreamDesk.sparseimage.zip"

# Mount StreamDesk.sparseimage
hdiutil mount "StreamDesk.sparseimage"

# remove old files
rm -rf "/Volumes/StreamDesk/StreamDesk.app" "/Volumes/StreamDesk/Release Notes.rtfd"

# copy new files
cp -r "StreamDesk.app" "/Volumes/StreamDesk/StreamDesk.app"
cp -r "Release Notes.rtfd" "/Volumes/StreamDesk/Release Notes.rtfd"

# unmount StreamDesk.sparseimage
hdiutil eject "/Volumes/StreamDesk"

# convert StreamDesk.sparseimage
hdiutil convert -format UDZO -o "StreamDesk.dmg" "StreamDesk.sparseimage"
