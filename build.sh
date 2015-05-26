#!/bin/bash
rm -rf artifacts
if ! type dnvm > /dev/null 2>&1; then
    curl -sSL https://raw.githubusercontent.com/aspnet/Home/dev/dnvminstall.sh | DNX_BRANCH=dev sh && source ~/.dnx/dnvm/dnvm.sh
fi
dnvm install 1.0.0-beta4
dnu restore
rc=$?; if [[ $rc != 0 ]]; then exit $rc; fi
cd tests/GholfReg.Domain.Tests
dnx . test -parallel none
rc=$?; if [[ $rc != 0 ]]; then exit $rc; fi
cd ../../
dnvm use 1.0.0-beta4
dnu publish src/GholfReg.Web --no-source --out artifacts/build/gholfreg --runtime dnx-mono.1.0.0-beta4 2>&1
# work around for kpm bundle returning an exit code 0 on failure
#grep "Build failed" buildlog
#rc=$?; if [[ $rc == 0 ]]; then exit 1; fi
