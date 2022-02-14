[<Measure>]
type Chicken


[<Struct>]
type Block<[<Measure>] 'Measure, 'T> =
    struct
        val Values: array<'T>
        new (v: array<'T>) = { Values = v }
        member inline this.Item
            with get (i: int<'Measure>) =
                this.Values[int i]

            and set (index: int<'Measure>) value =
                this.Values[int index] <- value

        member this.Length = LanguagePrimitives.Int32WithMeasure<'Measure> this.Values.Length

    end

let BlockOrdered (a: Block<Chicken, int>) =
    let mutable result = 0
    let mutable i = 0<Chicken>
    while i < a.Length - 1<Chicken> do
        result <- a[i]
        i <- i + 1<Chicken>
    result


let ArrayOrdered (a: array<int>) =
    let mutable result = 0
    let mutable i = 0
    while i < a.Length - 1 do
        result <- a[i]
        i <- i + 1
    result