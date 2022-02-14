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
        member this.Item
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
    let loopCount = 1_000_000
    let arraySize = 100
    let randomLookups =
        [|
            for _ = 1 to loopCount * arraySize do
                rng.Next (arraySize)
        |]

    let typedRandomLookups =
        randomLookups
        |> Array.map (fun x -> x * 1<Chicken>)

    let lookupArray =
        [| for _ = 1 to arraySize do rng.Next (0, 100) |]

    let block =
        lookupArray
        |> Block<Chicken, _>


    [<Benchmark>]
    member _.ArrayRandom () =
        let mutable result = 0
        let mutable i = 0

        while i < randomLookups.Length - 1 do
            let nextLookup = randomLookups[i]
            result <- lookupArray[nextLookup]
            i <- i + 1

        result


    [<Benchmark>]
    member _.BlockRandom () =
        let mutable result = 0
        let mutable i = 0

        while i < typedRandomLookups.Length - 1 do
            let nextLookup = typedRandomLookups[i]
            result <- block[nextLookup]
            i <- i + 1

        result


    [<Benchmark>]
    member _.ArrayOrdered () =
        let mutable result = 0

        for _ = 1 to loopCount do
            let mutable i = 0
            while i < lookupArray.Length - 1 do
                result <- lookupArray[i]
                i <- i + 1

        result


    [<Benchmark>]
    member _.BlockOrdered () =
        let mutable result = 0

        for _ = 1 to loopCount do
            let mutable i = 0<Chicken>
            while i < block.Length - 1<Chicken> do
                result <- block[i]
                i <- i + 1<Chicken>

        result


[<EntryPoint>]
let main argv =

    let summary = BenchmarkRunner.Run<Benchmarks>()

    1
