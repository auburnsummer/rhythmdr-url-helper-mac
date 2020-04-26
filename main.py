import json
import sys

import urllib.request

def main(args):
    if len(args) < 1:
        return 1

    data = {
        'command' : 'AddLevel',
        'ZipPath' : args[0].replace('rhythmdr://', ''),
        'filename' : args[0]
    }

    print(data)

    req = urllib.request.Request(
        'http://localhost:49812',
        json.dumps(data).encode('utf-8'),
        method='POST'
    )
    resp = urllib.request.urlopen(req)



if __name__ == '__main__':
    main(sys.argv[1:])