# Makefile for building MechJeb

ifeq ($(OS),Windows_NT)
	# do 'Doze stuff
else
	UNAME_S := $(shell uname -s)
	ifeq ($(UNAME_S),Linux)
		XDG_DATA_HOME := ${HOME}/.local/share
		KSPDIR := ${XDG_DATA_HOME}/Steam/SteamApps/common/Kerbal Space Program
		MANAGED := ../ksp-data/0.24/KSP_Data/Managed
	endif
	ifeq ($(UNAME_S),Darwin)
		KSPDIR  := ${HOME}/Library/Application Support/Steam/SteamApps/common/Kerbal Space Program
		MANAGED := ${KSPDIR}/KSP.app/Contents/Data/Managed/
	endif
endif

PROJECT=SCANsat-Notebook

SOURCEFILES := \
	$(wildcard source/${PROJECT}/*.cs) \
	$(wildcard source/${PROJECT}/Properties/*.cs) \

RESGEN2 := resgen2
GMCS    := gmcs
GIT     := git
TAR     := tar
ZIP     := zip

VERSION := $(shell ${GIT} describe --tags --always)
VERSION := 0.1.0.${BUILD_NUMBER}

all: info build

info:
	@echo "== SCANsat Notebook Build Information =="
	@echo "  version: ${VERSION}"
	@echo "  resgen2: ${RESGEN2}"
	@echo "  gmcs:    ${GMCS}"
	@echo "  git:     ${GIT}"
	@echo "  tar:     ${TAR}"
	@echo "  zip:     ${ZIP}"
	@echo "  KSP Data: ${KSPDIR}"
	@echo "========================================"

build: build/${PROJECT}.dll

build/%.dll: ${SOURCEFILES}
	mkdir -p build
#	${RESGEN2} -usesourcepath MechJeb2/Properties/Resources.resx build/Resources.resources
	${GMCS} -t:library -lib:"${MANAGED}" \
		-r:Assembly-CSharp,Assembly-CSharp-firstpass,UnityEngine \
		-out:$@ \
		${SOURCEFILES}
#		-resource:build/Resources.resources,MuMech.Properties.Resources.resources

package: build ${SOURCEFILES}
	mkdir -p package/${PROJECT}/Plugins
#	cp -r Parts package/MechJeb2/
	cp build/${PROJECT}.dll package/${PROJECT}/Plugins/
	cp README.md package/${PROJECT}/
	cp LICENSE package/${PROJECT}/

%.tar.gz:
	${TAR} zcf $@ package/${PROJECT}

tar.gz: package ${PROJECT}-${VERSION}.tar.gz

%.zip:
	${ZIP} -9 -r $@ package/${PROJECT}

zip: package ${PROJECT}-${VERSION}.zip


clean:
	@echo "Cleaning up build and package directories..."
	rm -rf build/ package/

# install: build
# 	mkdir -p "${KSPDIR}"/GameData/MechJeb2/Plugins
# 	cp -r Parts "${KSPDIR}"/GameData/MechJeb2/
# 	cp build/MechJeb2.dll "${KSPDIR}"/GameData/MechJeb2/Plugins/

# uninstall: info
# 	rm -rf "${KSPDIR}"/GameData/MechJeb2/Plugins
# 	rm -rf "${KSPDIR}"/GameData/MechJeb2/Parts


.PHONY : all info build package tar.gz zip clean install uninstall
