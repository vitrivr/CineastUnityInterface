"""
Utility to publish the unity package to make it available for upm.

For usage, invoke

$> python publish.py --help
"""
import argparse
import os

parser = argparse.ArgumentParser()

parser.add_argument("-r", "--release", help="The name of the release branch", default="release")
parser.add_argument("-i", "--init", help="Before the first push, one has to call init", action="store_true")

args = parser.parse_args()
baseCmd = 'git subtree push --prefix "Assets/CineastUnityInterface" origin'
cmd = baseCmd + ' ' + args.release

if args.init:
  addCmd = 'git subtree add --prefix "Assets/CineastUnityInterface" origin'
  os.system(addCmd + ' ' + args.release + ' --squash')

os.system(cmd)

print('Published!')