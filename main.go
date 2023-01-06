package main

import (
	"bufio"
	"fmt"
	"log"
	"os"
)

const (
	size = 30000
	null = '\u0000'
)

var (
	arr     = make([]rune, size)
	code    string
	arrPos  int
	codePos int
	isFile  bool
)

func eval(c rune) {
	switch c {
	case '>':
		arrPos++
	case '<':
		arrPos--
	case '+':
		arr[arrPos]++
	case '-':
		arr[arrPos]--
	case '.':
		fmt.Printf("%c", arr[arrPos])
	case ',':
		c, err := os.Stdin.Read([]byte{0})
		if err != nil {
			checkErr(err)
		} else {
			arr[arrPos] = rune(c)
		}
	}
}

func scan(skip bool) bool {
	for arrPos >= 0 && codePos < len(code) {
		if arrPos >= len(arr) {
			arr = append(arr, null)
		}
		c := rune(code[codePos])
		switch c {
		case '[':
			codePos++
			oldJ := codePos
			for scan(arr[arrPos] == null) {
				codePos = oldJ
			}
		case ']':
			return arr[arrPos] != null
		default:
			if !skip {
				eval(c)
			}
		}
		codePos++
	}
	return false
}

func scanFile(path string) {
	data, err := os.ReadFile(path)
	if err != nil {
		log.Fatal(err)
	}
	code = string(data)
	scan(false)
}

func repl() {
	var err error
	reader := bufio.NewReader(os.Stdin)
	for {
		fmt.Print("    ")
		code, err = reader.ReadString('\n')
		if err != nil {
			fmt.Println(err.Error())
		}
		scan(false)
	}
}

func checkErr(err error) {
	if isFile {
		log.Fatal(err)
	} else {
		fmt.Println(err.Error())
	}
}

func main() {
	args := os.Args
	if len(args) == 2 {
		isFile = true
		scanFile(args[1])
	} else {
		repl()
	}
}
