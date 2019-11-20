"""
Utility to publish the unity package to make it available for upm.

For usage, invoke

$> python publish.py --help

Thanks to https://github.com/neogeek/unity-package-example for providing an excellent example
"""
import argparse
import os

PACKAGE_DIR = 'Assets/CineastUnityInterface'

parser = argparse.ArgumentParser(description="Utility to publish the stand-alone unity package available for upm.")

parser.add_argument("-r", "--release", help="The name of the release branch", default="release")


args = parser.parse_args()

cmd = 'git subtree push --prefix "%s" origin %s' % (PACKAGE_DIR, args.release)

os.system(cmd)

print('Published!')
