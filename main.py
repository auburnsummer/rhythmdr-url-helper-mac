import json
import sys

def main(args):
    if len(args) < 1:
        return 1


    print("Here are you args:")
    print(args)
    with open("output.txt", "w") as f:
        f.write(json.dumps(args))

    input("Press any key to continue...")



if __name__ == '__main__':
    main(sys.argv[1:])