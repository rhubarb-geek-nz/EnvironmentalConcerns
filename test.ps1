#!/usr/bin/env pwsh
# Copyright (c) 2026 Roger Brown.
# Licensed under the MIT License.

trap
{
	throw $PSItem
}

$ErrorActionPreference = 'Stop'
$InformationPreference = 'Continue'

function Assert
{
	Param ([parameter(Mandatory=$true)][ScriptBlock]$assertion)

	if ( -not ( & $assertion ) )
	{
		throw $assertion
	}
}

$path = Expand-EnvironmentVariables %PATH%

Assert { $path -eq $env:PATH }
