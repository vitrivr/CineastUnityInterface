"""
Utility to publish the unity package to make it available for upm.

For usage, invoke

$> python publish.py --help

Thanks to https://github.com/neogeek/unity-package-example for providing an excellent example
"""
import argparse
import subprocess
import sys

PACKAGE_DIR = 'Assets/CineastUnityInterface'

parser = argparse.ArgumentParser(description="Utility to publish the stand-alone unity package available for upm.")

parser.add_argument("-r", "--release", help="The name of the release branch", default="release")
parser.add_argument("-b", "--branch", help="The name of the source branch (current if not specified)")

args = parser.parse_args()

if args.branch:
  branch = args.branch
else:
  result = subprocess.run(['git', 'branch', '--show-current'], stdout=subprocess.PIPE)
  branch = result.stdout.decode().strip()

result = subprocess.run(['git', 'subtree', 'split', '--prefix', f'{PACKAGE_DIR}', branch], stdout=subprocess.PIPE)

result = subprocess.run(['git', 'push', 'origin', f'{result.stdout.decode().strip()}:{args.release}', '--force'])

if result.returncode == 0:
  print('Published!')
else:
  print('ERROR: Could not publish!', file=sys.stderr)
