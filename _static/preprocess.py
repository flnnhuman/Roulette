import re
from argparse import ArgumentParser

vars_ = {}
pattern = r'\$\{[\w.-]*\}'

arg_parser = ArgumentParser()
arg_parser.add_argument('-v', type = str, help = 'File with variables.')
arg_parser.add_argument('-s', type = str, help = 'Source file to process.')
arg_parser.add_argument('-t', type = str, help = 'Target file to write lines to.')
args = arg_parser.parse_args()

with open(args.v, encoding = 'UTF-8') as values:
    for line in values:
        if line.startswith('#'):
            continue
        if '=' in line:
            key, value = line.replace(' ', '').replace('\n', '').split('=')
            vars_.update({key: value})

with open(args.s, encoding = 'UTF-8') as src, \
  open(args.t, mode = 'w', encoding = 'UTF-8') as target:
    for line in src:
        matches = re.findall(pattern, line)
        if len(matches) > 0:
            match, var_name = matches[0], matches[0][2:-1]
            target.write(line.replace(match, vars_[var_name]))
        else:
            target.write(line)
