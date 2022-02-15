open System
open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running
open BenchmarkDotNet.Diagnosers


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


[<HardwareCounters(HardwareCounter.BranchMispredictions, HardwareCounter.CacheMisses)>]
[<DisassemblyDiagnoser(printSource = true, exportHtml = true)>]
type Benchmarks () =

    let rng = Random 123
    let loopCount = 1
    let arraySize = 1_000
    let randomLookups =
        [|
            for _ = 1 to loopCount * arraySize do
                rng.Next (arraySize)
        |]

    let typedRandomLookups =
        randomLookups
        |> Array.map (fun x -> x * 1<Chicken>)

    let lookupValues =
        [| for _ = 1 to arraySize do rng.Next (0, 100) |]

    let arrayRandomLookupValues =
        lookupValues
        |> Array.copy

    let blockRandomLookupValues =
        lookupValues
        |> Array.copy
        |> Block<Chicken, _>

    let arrayOrderedLookupValues =
        lookupValues
        |> Array.copy

    let blockOrderedLookupValues =
        lookupValues
        |> Array.copy
        |> Block<Chicken, _>

    let arrayOrderedFast (a: array<int>) =
        let mutable result = 0
        let mutable i = 0
        while i < a.Length - 1 do
            result <- a[i]
            i <- i + 1
        result

    let blockOrderedFast (a: Block<Chicken, int>) =
        let mutable result = 0
        let mutable i = 0<Chicken>
        while i < a.Length - 1<Chicken> do
            result <- a[i]
            i <- i + 1<Chicken>
        result

    // [<Benchmark>]
    // member _.ArrayRandom () =
    //     let mutable result = 0
    //     let mutable i = 0

    //     while i < randomLookups.Length - 1 do
    //         let nextLookup = randomLookups[i]
    //         result <- arrayRandomLookupValues[nextLookup]
    //         i <- i + 1

    //     result


    // [<Benchmark>]
    // member _.BlockRandom () =
    //     let mutable result = 0
    //     let mutable i = 0

    //     while i < typedRandomLookups.Length - 1 do
    //         let nextLookup = typedRandomLookups[i]
    //         result <- blockRandomLookupValues[nextLookup]
    //         i <- i + 1

    //     result


    [<Benchmark>]
    member _.ArrayOrderedSlow () =
        let mutable result = 0
        let mutable i = 0
        while i < arrayOrderedLookupValues.Length - 1 do
            result <- arrayOrderedLookupValues[i]
            i <- i + 1
        result


    [<Benchmark>]
    member _.BlockOrderedSlow () =
        let mutable result = 0
        let mutable i = 0<Chicken>
        while i < blockOrderedLookupValues.Length - 1<Chicken> do
            result <- blockOrderedLookupValues[i]
            i <- i + 1<Chicken>
        result


    [<Benchmark>]
    member _.ArrayOrderedFast () =
        arrayOrderedFast arrayOrderedLookupValues


    [<Benchmark>]
    member _.BlockOrderedFast () =
        blockOrderedFast blockOrderedLookupValues


[<EntryPoint>]
let main argv =

    let summary = BenchmarkRunner.Run<Benchmarks>()

    1
