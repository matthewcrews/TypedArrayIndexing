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

// [<HardwareCounters(HardwareCounter.BranchMispredictions, HardwareCounter.CacheMisses)>]
// [<DisassemblyDiagnoser>]
// [<DisassemblyDiagnoser(printSource = true, exportHtml = true)>]
type Benchmarks () =

    let rng = Random 123
    let loopCount = 1
    let arraySize = 100
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
    member _.BlockOrdered () =
        let mutable result = 0
        let mutable i = 0<Chicken>
        while i < blockOrderedLookupValues.Length - 1<Chicken> do
            result <- blockOrderedLookupValues[i]
            i <- i + 1<Chicken>
        result


    [<Benchmark>]
    member _.ArrayOrdered () =
        let mutable result = 0
        let mutable i = 0
        while i < arrayOrderedLookupValues.Length - 1 do
            result <- arrayOrderedLookupValues[i]
            i <- i + 1
        result


[<EntryPoint>]
let main argv =

    let summary = BenchmarkRunner.Run<Benchmarks>()

    1
