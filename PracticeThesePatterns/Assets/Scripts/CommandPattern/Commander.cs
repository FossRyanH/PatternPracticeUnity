using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Commander : MonoBehaviour
{
    private PlanePointDetector _planePointDetector;
    private NavMeshAgent _navMeshAgent;

    private Queue<Command> _commands = new Queue<Command>();
    private Command _currentCommand;
    private ICommandStrategy _currentStrategy;
    
    // Keep all strategies under this comment
    private MoveCommandStrategy _moveStrategy;

    [SerializeField] private TMP_Text txtCurrentStrategy;

    private void Awake()
    {
        _planePointDetector = FindObjectOfType<PlanePointDetector>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

        if (!_planePointDetector || !_navMeshAgent) return;

        _moveStrategy = new MoveCommandStrategy(_navMeshAgent);

        _planePointDetector.OnPointDetected += OnPointDetected;
    }

    private void Update()
    {
        ProcessCommands();
    }

    private void ProcessCommands()
    {
        // If there is a current command and command is not complete yet, return
        if (_currentCommand != null && !_currentCommand.IsComplete) return;

        // If commands, return
        if (_commands.Count == 0) return;

        // Grab first command from the queue and execute it
        _currentCommand = _commands.Dequeue();
        _currentCommand.Execute();
    }

    private void OnPointDetected(Vector3 location)
    {
        if (_currentStrategy == null) return;
        
        // Create command and add to queue
        _commands.Enqueue(_currentStrategy.CreateCommand(location));
    }

    void SetMoveStrategy()
    {
        _currentStrategy = _moveStrategy;
        txtCurrentStrategy.text = $"{this.gameObject.name} is Moving";
    }
}
