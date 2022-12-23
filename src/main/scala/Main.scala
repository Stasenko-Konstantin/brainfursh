import scala.io.Source
import scala.io.StdIn.readLine
import scala.util.ChainingOps

object Main:
  private val size = 30000
  private val nil = '\u0000'
  var oldCode = ""
  var i = 0
  var arr = Iterator.unfold(Nil: List[Char])(a =>
    if a.length == size then
      None
    else
      Some(nil, a :+ nil)).toArray

  extension[A] (v: A)
    def |>[B](f: A => B): B = f(v)

  def eval(code: String): Unit = println(code)

  def scanFile(path: String): Unit =

    Source.fromFile(path).foldLeft("")(_ + _.toString) |> eval


  def repl(): Unit =
    print("    ")
    eval(readLine())

  def main(args: Array[String]): Unit =
    if args.length == 1 then
      scanFile(args(0))
    else
      repl()
